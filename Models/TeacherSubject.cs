using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class TeacherSubject
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        
        public Teacher Teacher { get; set; } = null!;
        
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; } = null!;
        
        [StringLength(255)]
        public string? Notes { get; set; }
        
        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
} 