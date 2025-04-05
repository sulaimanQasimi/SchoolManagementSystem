using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class ExamResult
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        
        public Exam Exam { get; set; } = null!;
        
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        
        public Student Student { get; set; } = null!;
        
        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; } = null!;
        
        [Required]
        [Range(0, 100)]
        public decimal MarksObtained { get; set; }
        
        [StringLength(10)]
        public string? Grade { get; set; }
        
        [StringLength(255)]
        public string? Remarks { get; set; }
        
        public DateTime RecordedAt { get; set; } = DateTime.Now;
        
        [StringLength(50)]
        public string RecordedBy { get; set; } = string.Empty;
    }
} 