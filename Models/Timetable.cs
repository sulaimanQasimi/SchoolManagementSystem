using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Timetable
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        
        public Section Section { get; set; } = null!;
        
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; } = null!;
        
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        
        public Teacher Teacher { get; set; } = null!;
        
        public DayOfWeek Day { get; set; }
        
        [Required]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        public TimeSpan EndTime { get; set; }
        
        [StringLength(100)]
        public string? Room { get; set; }
    }
} 