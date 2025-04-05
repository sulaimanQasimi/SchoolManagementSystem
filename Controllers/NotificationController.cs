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
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Notification
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));
            
            IQueryable<Notification> notifications = _context.Notifications
                .OrderByDescending(n => n.CreatedAt);
                
            // Filter notifications based on role
            if (userRoles.Contains("Student"))
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.UserId == userId);
                    
                if (student != null)
                {
                    notifications = notifications.Where(n => 
                        n.RecipientType == "All" || 
                        n.RecipientType == "Students" || 
                        (n.RecipientType == "Class" && n.ClassId == student.ClassId) ||
                        (n.RecipientType == "Section" && n.SectionId == student.SectionId) ||
                        (n.RecipientType == "Individual" && n.RecipientId == student.Id.ToString()));
                }
            }
            else if (userRoles.Contains("Teacher"))
            {
                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(t => t.UserId == userId);
                    
                if (teacher != null)
                {
                    notifications = notifications.Where(n => 
                        n.RecipientType == "All" || 
                        n.RecipientType == "Teachers" ||
                        (n.RecipientType == "Individual" && n.RecipientId == teacher.Id.ToString()));
                }
            }
            else if (userRoles.Contains("Parent"))
            {
                var parent = await _context.Parents
                    .FirstOrDefaultAsync(p => p.UserId == userId);
                    
                if (parent != null)
                {
                    notifications = notifications.Where(n => 
                        n.RecipientType == "All" || 
                        n.RecipientType == "Parents" ||
                        (n.RecipientType == "Individual" && n.RecipientId == parent.Id.ToString()));
                }
            }
            
            return View(await notifications.ToListAsync());
        }

        // GET: Notification/Create
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create()
        {
            var classes = await _context.Classes.OrderBy(c => c.Name).ToListAsync();
            ViewData["Classes"] = new SelectList(classes, "Id", "Name");
            
            return View();
        }

        // POST: Notification/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(Notification notification, string recipientType, int? classId, int? sectionId)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                
                notification.SenderId = userId;
                notification.SenderName = $"{user.FirstName} {user.LastName}";
                notification.CreatedAt = DateTime.Now;
                notification.RecipientType = recipientType;
                
                if (recipientType == "Class" && classId.HasValue)
                {
                    notification.ClassId = classId;
                }
                
                if (recipientType == "Section" && sectionId.HasValue)
                {
                    notification.ClassId = classId;
                    notification.SectionId = sectionId;
                }
                
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var classes = await _context.Classes.OrderBy(c => c.Name).ToListAsync();
            ViewData["Classes"] = new SelectList(classes, "Id", "Name", classId);
            
            if (classId.HasValue)
            {
                var sections = await _context.Sections
                    .Where(s => s.ClassId == classId)
                    .OrderBy(s => s.Name)
                    .ToListAsync();
                    
                ViewData["Sections"] = new SelectList(sections, "Id", "Name", sectionId);
            }
            
            return View(notification);
        }

        // GET: Notification/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // GET: Notification/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // POST: Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Notification/GetSections
        [HttpGet]
        public async Task<IActionResult> GetSections(int classId)
        {
            var sections = await _context.Sections
                .Where(s => s.ClassId == classId)
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();
                
            return Json(sections);
        }
    }
}