using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class ExamResult
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        
        public Exam Exam { get; set; } = null!;
        
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        
        public Student Student { get; set; } = null!;
        
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; } = null!;
        
        public decimal MarksObtained { get; set; }
        
        [StringLength(10)]
        public string? Grade { get; set; }
        
        [StringLength(255)]
        public string? Remarks { get; set; }
        
        public DateTime EnteredOn { get; set; } = DateTime.Now;
        
        [ForeignKey("EnteredBy")]
        public string? EnteredById { get; set; }
        
        public ApplicationUser? EnteredBy { get; set; }
    }
} 