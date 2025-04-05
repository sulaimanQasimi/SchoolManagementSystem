using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Security.Claims;

namespace SchoolManagementSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            var dashboard = new Dashboard
            {
                TotalStudents = await _context.Students.CountAsync(),
                TotalTeachers = await _context.Teachers.CountAsync(),
                TotalClasses = await _context.Classes.CountAsync(),
                TotalParents = await _context.Parents.CountAsync()
            };

            // Get upcoming exams (next 30 days)
            dashboard.UpcomingExams = await _context.Exams
                .Where(e => e.ExamDate >= DateTime.Now && e.ExamDate <= DateTime.Now.AddDays(30))
                .OrderBy(e => e.ExamDate)
                .Take(5)
                .ToListAsync();

            // Get recent notifications
            dashboard.RecentNotifications = await _context.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .Take(5)
                .ToListAsync();

            // Role specific data
            if (userRoles.Contains("Admin"))
            {
                // Additional admin stats if needed
            }
            else if (userRoles.Contains("Teacher"))
            {
                var teacher = await _context.Teachers
                    .Include(t => t.ClassTeacherOfClass)
                    .Include(t => t.ClassTeacherOfSection)
                    .FirstOrDefaultAsync(t => t.UserId == userId);

                if (teacher != null)
                {
                    // Get classes taught by this teacher
                    dashboard.TeacherClasses = await _context.TeacherSubjects
                        .Where(ts => ts.TeacherId == teacher.Id)
                        .Select(ts => ts.Subject.Class)
                        .Distinct()
                        .ToListAsync();
                }
            }
            else if (userRoles.Contains("Student"))
            {
                var student = await _context.Students
                    .Include(s => s.Class)
                    .Include(s => s.Section)
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                if (student != null)
                {
                    // Get student specific exams
                    dashboard.UpcomingExams = await _context.Exams
                        .Where(e => e.ClassId == student.ClassId && e.ExamDate >= DateTime.Now)
                        .OrderBy(e => e.ExamDate)
                        .Take(5)
                        .ToListAsync();
                }
            }
            else if (userRoles.Contains("Parent"))
            {
                var parent = await _context.Parents
                    .FirstOrDefaultAsync(p => p.UserId == userId);

                if (parent != null)
                {
                    // Get children of this parent
                    dashboard.ParentStudents = await _context.Students
                        .Where(s => s.ParentId == parent.Id)
                        .Include(s => s.Class)
                        .Include(s => s.Section)
                        .ToListAsync();
                }
            }

            return View(dashboard);
        }
    }
} 