using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        
        public Student? Student { get; set; }
        
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        
        public Teacher? Teacher { get; set; }
        
        [Required]
        public DateTime Date { get; set; } = DateTime.Today;
        
        [Required]
        public bool IsPresent { get; set; }
        
        [StringLength(255)]
        public string? Remarks { get; set; }
        
        public DateTime RecordedAt { get; set; } = DateTime.Now;
        
        [StringLength(50)]
        public string RecordedBy { get; set; } = string.Empty;
    }
} 