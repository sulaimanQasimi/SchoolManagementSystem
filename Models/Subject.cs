using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        public int PassingMarks { get; set; } = 35;
        
        public int FullMarks { get; set; } = 100;
        
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
        
        public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
        
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
} 