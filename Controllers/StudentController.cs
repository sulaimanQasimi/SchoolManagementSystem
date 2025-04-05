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
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Class)
                .Include(s => s.Section)
                .Include(s => s.Parent)
                    .ThenInclude(p => p.User)
                .ToListAsync();

            return View(students);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Class)
                .Include(s => s.Section)
                .Include(s => s.Parent)
                    .ThenInclude(p => p.User)
                .Include(s => s.Attendances)
                .Include(s => s.ExamResults)
                    .ThenInclude(e => e.Exam)
                .Include(s => s.ExamResults)
                    .ThenInclude(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["ParentId"] = new SelectList(
                await _context.Parents
                    .Include(p => p.User)
                    .Select(p => new { p.Id, FullName = p.User.FirstName + " " + p.User.LastName })
                    .ToListAsync(),
                "Id", "FullName");
            
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Student student, string email, string password, string firstName, string lastName, DateTime dateOfBirth)
        {
            if (ModelState.IsValid)
            {
                // Create user first
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    JoinDate = DateTime.Now,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // Add to student role
                    await _userManager.AddToRoleAsync(user, "Student");
                    
                    // Create student profile
                    student.UserId = user.Id;
                    
                    // If a class is selected, get the sections for that class
                    if (student.ClassId.HasValue)
                    {
                        var sections = await _context.Sections
                            .Where(s => s.ClassId == student.ClassId)
                            .ToListAsync();
                        
                        if (sections.Any() && !student.SectionId.HasValue)
                        {
                            student.SectionId = sections.First().Id;
                        }
                    }
                    
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", student.ClassId);
            if (student.ClassId.HasValue)
            {
                ViewData["SectionId"] = new SelectList(
                    _context.Sections.Where(s => s.ClassId == student.ClassId), 
                    "Id", "Name", student.SectionId);
            }
            ViewData["ParentId"] = new SelectList(
                await _context.Parents
                    .Include(p => p.User)
                    .Select(p => new { p.Id, FullName = p.User.FirstName + " " + p.User.LastName })
                    .ToListAsync(),
                "Id", "FullName", student.ParentId);
                
            return View(student);
        }

        // GET: Student/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
                
            if (student == null)
            {
                return NotFound();
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", student.ClassId);
            if (student.ClassId.HasValue)
            {
                ViewData["SectionId"] = new SelectList(
                    _context.Sections.Where(s => s.ClassId == student.ClassId), 
                    "Id", "Name", student.SectionId);
            }
            else
            {
                ViewData["SectionId"] = new SelectList(
                    Enumerable.Empty<SelectListItem>(), "Id", "Name");
            }
            
            ViewData["ParentId"] = new SelectList(
                await _context.Parents
                    .Include(p => p.User)
                    .Select(p => new { p.Id, FullName = p.User.FirstName + " " + p.User.LastName })
                    .ToListAsync(),
                "Id", "FullName", student.ParentId);
                
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Student student, string firstName, string lastName)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStudent = await _context.Students
                        .Include(s => s.User)
                        .FirstOrDefaultAsync(s => s.Id == id);
                        
                    if (existingStudent == null)
                    {
                        return NotFound();
                    }
                    
                    // Update user info
                    existingStudent.User.FirstName = firstName;
                    existingStudent.User.LastName = lastName;
                    
                    // Update student properties
                    existingStudent.AdmissionNumber = student.AdmissionNumber;
                    existingStudent.EnrollmentDate = student.EnrollmentDate;
                    existingStudent.ClassId = student.ClassId;
                    existingStudent.SectionId = student.SectionId;
                    existingStudent.ParentId = student.ParentId;
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", student.ClassId);
            if (student.ClassId.HasValue)
            {
                ViewData["SectionId"] = new SelectList(
                    _context.Sections.Where(s => s.ClassId == student.ClassId), 
                    "Id", "Name", student.SectionId);
            }
            ViewData["ParentId"] = new SelectList(
                await _context.Parents
                    .Include(p => p.User)
                    .Select(p => new { p.Id, FullName = p.User.FirstName + " " + p.User.LastName })
                    .ToListAsync(),
                "Id", "FullName", student.ParentId);
                
            return View(student);
        }

        // GET: Student/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Class)
                .Include(s => s.Section)
                .Include(s => s.Parent)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
                
            if (student == null)
            {
                return NotFound();
            }
            
            var user = student.User;
            
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            
            // Optionally, delete the user account as well
            // Uncomment if you want to delete the user when deleting the student
            // await _userManager.DeleteAsync(user);
            
            return RedirectToAction(nameof(Index));
        }
        
        // GET: GetSections
        [HttpGet]
        public async Task<IActionResult> GetSections(int classId)
        {
            var sections = await _context.Sections
                .Where(s => s.ClassId == classId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();
                
            return Json(sections);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
} 