using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; } = DateTime.Today;
        
        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        
        public Student? Student { get; set; }
        
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        
        public Teacher? Teacher { get; set; }
        
        public bool IsPresent { get; set; }
        
        [StringLength(255)]
        public string? Remarks { get; set; }
        
        public AttendanceType AttendanceType { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("CreatedBy")]
        public string? CreatedById { get; set; }
        
        public ApplicationUser? CreatedBy { get; set; }
    }
    
    public enum AttendanceType
    {
        Student,
        Teacher
    }
} 