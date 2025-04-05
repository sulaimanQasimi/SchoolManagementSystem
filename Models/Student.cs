using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string AdmissionNumber { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        
        public ApplicationUser User { get; set; } = null!;
        
        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        
        public Class? Class { get; set; }
        
        [ForeignKey("Section")]
        public int? SectionId { get; set; }
        
        public Section? Section { get; set; }
        
        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        
        public Parent? Parent { get; set; }
        
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
} 