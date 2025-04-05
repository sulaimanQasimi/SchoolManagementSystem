using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Security.Claims;

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendanceController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Attendance
        public async Task<IActionResult> Index(DateTime? date, int? classId, int? sectionId)
        {
            date ??= DateTime.Today;
            
            var classes = await _context.Classes.OrderBy(c => c.Name).ToListAsync();
            ViewData["Classes"] = new SelectList(classes, "Id", "Name", classId);
            
            List<Section> sections = new List<Section>();
            if (classId.HasValue)
            {
                sections = await _context.Sections
                    .Where(s => s.ClassId == classId)
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            ViewData["Sections"] = new SelectList(sections, "Id", "Name", sectionId);
            
            ViewData["Date"] = date.Value.ToString("yyyy-MM-dd");
            
            var attendances = new List<Attendance>();
            
            if (classId.HasValue)
            {
                var query = _context.Attendances
                    .Include(a => a.Student)
                        .ThenInclude(s => s.User)
                    .Include(a => a.Teacher)
                        .ThenInclude(t => t.User)
                    .Where(a => a.Date.Date == date.Value.Date);
                
                if (sectionId.HasValue)
                {
                    attendances = await query
                        .Where(a => a.Student.ClassId == classId && a.Student.SectionId == sectionId)
                        .OrderBy(a => a.Student.User.FirstName)
                        .ToListAsync();
                }
                else
                {
                    attendances = await query
                        .Where(a => a.Student.ClassId == classId)
                        .OrderBy(a => a.Student.User.FirstName)
                        .ToListAsync();
                }
            }
            
            return View(attendances);
        }

        // GET: Attendance/MarkAttendance
        public async Task<IActionResult> MarkAttendance(int? classId, int? sectionId, DateTime? date)
        {
            date ??= DateTime.Today;
            
            var classes = await _context.Classes.OrderBy(c => c.Name).ToListAsync();
            ViewData["Classes"] = new SelectList(classes, "Id", "Name", classId);
            
            List<Section> sections = new List<Section>();
            if (classId.HasValue)
            {
                sections = await _context.Sections
                    .Where(s => s.ClassId == classId)
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            ViewData["Sections"] = new SelectList(sections, "Id", "Name", sectionId);
            
            ViewData["Date"] = date.Value.ToString("yyyy-MM-dd");
            
            var students = new List<Student>();
            
            if (classId.HasValue && sectionId.HasValue)
            {
                students = await _context.Students
                    .Include(s => s.User)
                    .Include(s => s.Attendances.Where(a => a.Date.Date == date.Value.Date))
                    .Where(s => s.ClassId == classId && s.SectionId == sectionId)
                    .OrderBy(s => s.User.FirstName)
                    .ToListAsync();
            }
            else if (classId.HasValue)
            {
                students = await _context.Students
                    .Include(s => s.User)
                    .Include(s => s.Attendances.Where(a => a.Date.Date == date.Value.Date))
                    .Where(s => s.ClassId == classId)
                    .OrderBy(s => s.User.FirstName)
                    .ToListAsync();
            }
            
            return View(students);
        }

        // POST: Attendance/SaveAttendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAttendance(int[] studentIds, bool[] isPresent, string date, string remarks)
        {
            if (studentIds.Length != isPresent.Length)
            {
                return BadRequest("Invalid data submitted");
            }
            
            var attendanceDate = DateTime.Parse(date);
            var teacherId = await GetCurrentTeacherId();
            
            if (teacherId == 0)
            {
                return Unauthorized("Only teachers can mark attendance");
            }
            
            // Delete existing attendance records for this date and these students
            var existingAttendances = await _context.Attendances
                .Where(a => a.Date.Date == attendanceDate.Date && studentIds.Contains(a.StudentId))
                .ToListAsync();
                
            if (existingAttendances.Any())
            {
                _context.Attendances.RemoveRange(existingAttendances);
            }
            
            // Create new attendance records
            for (int i = 0; i < studentIds.Length; i++)
            {
                var attendance = new Attendance
                {
                    StudentId = studentIds[i],
                    TeacherId = teacherId,
                    Date = attendanceDate,
                    IsPresent = isPresent[i],
                    Remarks = remarks
                };
                
                _context.Attendances.Add(attendance);
            }
            
            await _context.SaveChangesAsync();
            
            // Redirect back to the attendance page
            return RedirectToAction(nameof(Index), new { date = attendanceDate.ToString("yyyy-MM-dd") });
        }
        
        // GET: Attendance/Report
        public async Task<IActionResult> Report(int? studentId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Today.AddDays(-30);
            endDate ??= DateTime.Today;
            
            var students = await _context.Students
                .Include(s => s.User)
                .OrderBy(s => s.User.FirstName)
                .ToListAsync();
                
            ViewData["Students"] = new SelectList(students, "Id", "User.FirstName", studentId);
            ViewData["StartDate"] = startDate.Value.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate.Value.ToString("yyyy-MM-dd");
            
            var attendances = new List<Attendance>();
            
            if (studentId.HasValue)
            {
                attendances = await _context.Attendances
                    .Include(a => a.Student)
                        .ThenInclude(s => s.User)
                    .Include(a => a.Teacher)
                        .ThenInclude(t => t.User)
                    .Where(a => a.StudentId == studentId && 
                           a.Date.Date >= startDate.Value.Date && 
                           a.Date.Date <= endDate.Value.Date)
                    .OrderByDescending(a => a.Date)
                    .ToListAsync();
            }
            
            return View(attendances);
        }
        
        // GET: Attendance/GetSections
        [HttpGet]
        public async Task<IActionResult> GetSections(int classId)
        {
            var sections = await _context.Sections
                .Where(s => s.ClassId == classId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();
                
            return Json(sections);
        }
        
        private async Task<int> GetCurrentTeacherId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.UserId == userId);
                
            return teacher?.Id ?? 0;
        }
    }
} 