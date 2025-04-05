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
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExamController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Exam
        public async Task<IActionResult> Index()
        {
            var exams = await _context.Exams
                .Include(e => e.Class)
                .Include(e => e.Subject)
                .OrderByDescending(e => e.ExamDate)
                .ToListAsync();
                
            return View(exams);
        }

        // GET: Exam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Class)
                .Include(e => e.Subject)
                .Include(e => e.Results)
                    .ThenInclude(r => r.Student)
                        .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exam/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            
            return View();
        }

        // POST: Exam/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                
                // Create a notification about the new exam
                var notification = new Notification
                {
                    Title = $"New Exam: {exam.Title}",
                    Message = $"A new exam {exam.Title} has been scheduled for {exam.ExamDate.ToShortDateString()} from {exam.StartTime.ToString("hh:mm tt")} to {exam.EndTime.ToString("hh:mm tt")}.",
                    SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    SenderName = "Admin",
                    RecipientType = "Class",
                    ClassId = exam.ClassId,
                    CreatedAt = DateTime.Now,
                    IsImportant = true
                };
                
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", exam.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", exam.SubjectId);
            
            return View(exam);
        }

        // GET: Exam/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", exam.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", exam.SubjectId);
            
            return View(exam);
        }

        // POST: Exam/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", exam.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", exam.SubjectId);
            
            return View(exam);
        }

        // GET: Exam/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Class)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Exam/EnterMarks/5
        public async Task<IActionResult> EnterMarks(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var exam = await _context.Exams
                .Include(e => e.Class)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (exam == null)
            {
                return NotFound();
            }
            
            var students = await _context.Students
                .Include(s => s.User)
                .Include(s => s.ExamResults.Where(er => er.ExamId == id))
                .Where(s => s.ClassId == exam.ClassId)
                .OrderBy(s => s.User.FirstName)
                .ToListAsync();
                
            ViewData["Exam"] = exam;
            
            return View(students);
        }
        
        // POST: Exam/SaveMarks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveMarks(int examId, int[] studentIds, float[] marks, string[] grades, string[] remarks)
        {
            if (studentIds.Length != marks.Length || studentIds.Length != grades.Length || studentIds.Length != remarks.Length)
            {
                return BadRequest("Invalid data submitted");
            }
            
            var exam = await _context.Exams
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.Id == examId);
                
            if (exam == null)
            {
                return NotFound();
            }
            
            // Delete existing marks for this exam
            var existingResults = await _context.ExamResults
                .Where(er => er.ExamId == examId && studentIds.Contains(er.StudentId))
                .ToListAsync();
                
            if (existingResults.Any())
            {
                _context.ExamResults.RemoveRange(existingResults);
            }
            
            // Create new results
            for (int i = 0; i < studentIds.Length; i++)
            {
                var result = new ExamResult
                {
                    ExamId = examId,
                    StudentId = studentIds[i],
                    SubjectId = exam.SubjectId ?? 0,
                    MarksObtained = marks[i],
                    Grade = grades[i],
                    Remarks = remarks[i]
                };
                
                _context.ExamResults.Add(result);
            }
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Details), new { id = examId });
        }
        
        // GET: Exam/Results (for parents and students)
        [Authorize(Roles = "Student,Parent")]
        public async Task<IActionResult> Results()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            
            List<ExamResult> results = new List<ExamResult>();
            
            if (userRoles.Contains("Student"))
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.UserId == userId);
                    
                if (student != null)
                {
                    results = await _context.ExamResults
                        .Include(er => er.Exam)
                        .Include(er => er.Subject)
                        .Where(er => er.StudentId == student.Id)
                        .OrderByDescending(er => er.Exam.ExamDate)
                        .ToListAsync();
                }
            }
            else if (userRoles.Contains("Parent"))
            {
                var parent = await _context.Parents
                    .FirstOrDefaultAsync(p => p.UserId == userId);
                    
                if (parent != null)
                {
                    var studentIds = await _context.Students
                        .Where(s => s.ParentId == parent.Id)
                        .Select(s => s.Id)
                        .ToListAsync();
                        
                    results = await _context.ExamResults
                        .Include(er => er.Exam)
                        .Include(er => er.Subject)
                        .Include(er => er.Student)
                            .ThenInclude(s => s.User)
                        .Where(er => studentIds.Contains(er.StudentId))
                        .OrderByDescending(er => er.Exam.ExamDate)
                        .ToListAsync();
                }
            }
            
            return View(results);
        }

        // GET: Exam/GetSubjectsByClass
        [HttpGet]
        public async Task<IActionResult> GetSubjectsByClass(int classId)
        {
            var subjects = await _context.Subjects
                .Where(s => s.ClassId == classId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();
                
            return Json(subjects);
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
} 