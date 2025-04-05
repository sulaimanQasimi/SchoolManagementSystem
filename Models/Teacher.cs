using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string EmployeeId { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        
        public ApplicationUser User { get; set; } = null!;
        
        [StringLength(50)]
        public string? Qualification { get; set; }
        
        [StringLength(100)]
        public string? Specialization { get; set; }
        
        public decimal Salary { get; set; }
        
        [ForeignKey("Class")]
        public int? ClassTeacherOfClassId { get; set; }
        
        public Class? ClassTeacherOfClass { get; set; }
        
        [ForeignKey("Section")]
        public int? ClassTeacherOfSectionId { get; set; }
        
        public Section? ClassTeacherOfSection { get; set; }
        
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
} 