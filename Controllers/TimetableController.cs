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
    [Authorize]
    public class TimetableController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TimetableController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Timetable
        public async Task<IActionResult> Index(int? classId, int? sectionId)
        {
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
            
            var timetables = new List<Timetable>();
            
            // For Admin and Teacher, show timetable based on selected class/section
            if (User.IsInRole("Admin") || User.IsInRole("Teacher"))
            {
                if (classId.HasValue)
                {
                    var query = _context.Timetables
                        .Include(t => t.Class)
                        .Include(t => t.Section)
                        .Include(t => t.Subject)
                        .Include(t => t.Teacher)
                            .ThenInclude(t => t.User)
                        .Where(t => t.ClassId == classId);
                        
                    if (sectionId.HasValue)
                    {
                        timetables = await query
                            .Where(t => t.SectionId == sectionId)
                            .OrderBy(t => t.DayOfWeek)
                            .ThenBy(t => t.StartTime)
                            .ToListAsync();
                    }
                    else
                    {
                        timetables = await query
                            .OrderBy(t => t.DayOfWeek)
                            .ThenBy(t => t.StartTime)
                            .ToListAsync();
                    }
                }
            }
            // For Students, show their class timetable
            else if (User.IsInRole("Student"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.UserId == userId);
                    
                if (student != null && student.ClassId.HasValue)
                {
                    var query = _context.Timetables
                        .Include(t => t.Class)
                        .Include(t => t.Section)
                        .Include(t => t.Subject)
                        .Include(t => t.Teacher)
                            .ThenInclude(t => t.User)
                        .Where(t => t.ClassId == student.ClassId);
                        
                    if (student.SectionId.HasValue)
                    {
                        timetables = await query
                            .Where(t => t.SectionId == student.SectionId)
                            .OrderBy(t => t.DayOfWeek)
                            .ThenBy(t => t.StartTime)
                            .ToListAsync();
                    }
                    else
                    {
                        timetables = await query
                            .OrderBy(t => t.DayOfWeek)
                            .ThenBy(t => t.StartTime)
                            .ToListAsync();
                    }
                    
                    // Set the selected class and section for the dropdown
                    classId = student.ClassId;
                    sectionId = student.SectionId;
                    
                    ViewData["Classes"] = new SelectList(classes, "Id", "Name", classId);
                    
                    if (classId.HasValue)
                    {
                        sections = await _context.Sections
                            .Where(s => s.ClassId == classId)
                            .OrderBy(s => s.Name)
                            .ToListAsync();
                            
                        ViewData["Sections"] = new SelectList(sections, "Id", "Name", sectionId);
                    }
                }
            }
            
            return View(timetables);
        }

        // GET: Timetable/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(
                await _context.Teachers
                    .Include(t => t.User)
                    .Select(t => new { t.Id, FullName = t.User.FirstName + " " + t.User.LastName })
                    .ToListAsync(),
                "Id", "FullName");
                
            return View();
        }

        // POST: Timetable/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Timetable timetable)
        {
            if (ModelState.IsValid)
            {
                // Check for conflicting timetable entries
                var conflicts = await _context.Timetables
                    .Where(t => t.ClassId == timetable.ClassId && 
                           t.SectionId == timetable.SectionId && 
                           t.DayOfWeek == timetable.DayOfWeek &&
                           ((t.StartTime <= timetable.StartTime && t.EndTime > timetable.StartTime) ||
                            (t.StartTime < timetable.EndTime && t.EndTime >= timetable.EndTime) ||
                            (t.StartTime >= timetable.StartTime && t.EndTime <= timetable.EndTime)))
                    .ToListAsync();
                    
                if (conflicts.Any())
                {
                    ModelState.AddModelError(string.Empty, "There is a time conflict with an existing timetable entry for this class/section.");
                }
                else
                {
                    _context.Add(timetable);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { classId = timetable.ClassId, sectionId = timetable.SectionId });
                }
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", timetable.ClassId);
            
            if (timetable.ClassId.HasValue)
            {
                ViewData["SectionId"] = new SelectList(
                    _context.Sections.Where(s => s.ClassId == timetable.ClassId), 
                    "Id", "Name", timetable.SectionId);
                    
                ViewData["SubjectId"] = new SelectList(
                    _context.Subjects.Where(s => s.ClassId == timetable.ClassId), 
                    "Id", "Name", timetable.SubjectId);
            }
            
            ViewData["TeacherId"] = new SelectList(
                await _context.Teachers
                    .Include(t => t.User)
                    .Select(t => new { t.Id, FullName = t.User.FirstName + " " + t.User.LastName })
                    .ToListAsync(),
                "Id", "FullName", timetable.TeacherId);
                
            return View(timetable);
        }

        // GET: Timetable/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", timetable.ClassId);
            
            if (timetable.ClassId.HasValue)
            {
                ViewData["SectionId"] = new SelectList(
                    _context.Sections.Where(s => s.ClassId == timetable.ClassId), 
                    "Id", "Name", timetable.SectionId);
                    
                ViewData["SubjectId"] = new SelectList(
                    _context.Subjects.Where(s => s.ClassId == timetable.ClassId), 
                    "Id", "Name", timetable.SubjectId);
            }
            
            ViewData["TeacherId"] = new SelectList(
                await _context.Teachers
                    .Include(t => t.User)
                    .Select(t => new { t.Id, FullName = t.User.FirstName + " " + t.User.LastName })
                    .ToListAsync(),
                "Id", "FullName", timetable.TeacherId);
                
            return View(timetable);
        }

        // POST: Timetable/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Timetable timetable)
        {
            if (id != timetable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check for conflicting timetable entries
                var conflicts = await _context.Timetables
                    .Where(t => t.Id != timetable.Id && 
                           t.ClassId == timetable.ClassId && 
                           t.SectionId == timetable.SectionId && 
                           t.DayOfWeek == timetable.DayOfWeek &&
                           ((t.StartTime <= timetable.StartTime && t.EndTime > timetable.StartTime) ||
                            (t.StartTime < timetable.EndTime && t.EndTime >= timetable.EndTime) ||
                            (t.StartTime >= timetable.StartTime && t.EndTime <= timetable.EndTime)))
                    .ToListAsync();
                    
                if (conflicts.Any())
                {
                    ModelState.AddModelError(string.Empty, "There is a time conflict with an existing timetable entry for this class/section.");
                }
                else
                {
                    try
                    {
                        _context.Update(timetable);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TimetableExists(timetable.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index), new { classId = timetable.ClassId, sectionId = timetable.SectionId });
                }
            }
            
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", timetable.ClassId);
            
            if (timetable.ClassId.HasValue)
            {
                ViewData["SectionId"] = new SelectList(
                    _context.Sections.Where(s => s.ClassId == timetable.ClassId), 
                    "Id", "Name", timetable.SectionId);
                    
                ViewData["SubjectId"] = new SelectList(
                    _context.Subjects.Where(s => s.ClassId == timetable.ClassId), 
                    "Id", "Name", timetable.SubjectId);
            }
            
            ViewData["TeacherId"] = new SelectList(
                await _context.Teachers
                    .Include(t => t.User)
                    .Select(t => new { t.Id, FullName = t.User.FirstName + " " + t.User.LastName })
                    .ToListAsync(),
                "Id", "FullName", timetable.TeacherId);
                
            return View(timetable);
        }

        // GET: Timetable/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetables
                .Include(t => t.Class)
                .Include(t => t.Section)
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                    .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        // POST: Timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable != null)
            {
                _context.Timetables.Remove(timetable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Timetable/GetSections
        [HttpGet]
        public async Task<IActionResult> GetSections(int classId)
        {
            var sections = await _context.Sections
                .Where(s => s.ClassId == classId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();
                
            return Json(sections);
        }

        // GET: Timetable/GetSubjects
        [HttpGet]
        public async Task<IActionResult> GetSubjects(int classId)
        {
            var subjects = await _context.Subjects
                .Where(s => s.ClassId == classId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();
                
            return Json(subjects);
        }

        private bool TimetableExists(int id)
        {
            return _context.Timetables.Any(e => e.Id == id);
        }
    }
}