using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        
        public int TotalTeachers { get; set; }
        
        public int TotalClasses { get; set; }
        
        public int TotalSubjects { get; set; }
        
        public int TotalParents { get; set; }
        
        public int TotalAttendanceToday { get; set; }
        
        public int PresentStudentsToday { get; set; }
        
        public int PresentTeachersToday { get; set; }
        
        public decimal AttendancePercentage { get; set; }
        
        public IEnumerable<Exam> UpcomingExams { get; set; } = new List<Exam>();
        
        public IEnumerable<Notification> RecentNotifications { get; set; } = new List<Notification>();
    }
} 