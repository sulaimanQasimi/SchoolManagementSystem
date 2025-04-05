using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        public ExamType Type { get; set; }
        
        public bool IsPublished { get; set; } = false;
        
        public virtual ICollection<ExamResult> Results { get; set; } = new List<ExamResult>();
    }
    
    public enum ExamType
    {
        MidTerm,
        Final,
        Quiz,
        Assignment,
        Project
    }
} 