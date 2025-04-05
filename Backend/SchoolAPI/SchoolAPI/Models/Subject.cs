using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
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
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        [Range(0, 100)]
        public int? PassingMarks { get; set; }
        
        [Range(0, 100)]
        public int? FullMarks { get; set; }
        
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
        
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
} 