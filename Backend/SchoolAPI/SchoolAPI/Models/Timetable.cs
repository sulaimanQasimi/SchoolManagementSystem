using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class Timetable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        [Required]
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        
        public Section Section { get; set; } = null!;
        
        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; } = null!;
        
        [Required]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        
        public Teacher Teacher { get; set; } = null!;
        
        [Required]
        [Range(1, 7)]
        public int DayOfWeek { get; set; }
        
        [Required]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        public TimeSpan EndTime { get; set; }
        
        [StringLength(50)]
        public string? Room { get; set; }
    }
} 