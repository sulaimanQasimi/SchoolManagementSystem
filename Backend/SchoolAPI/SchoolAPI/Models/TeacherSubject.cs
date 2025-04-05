using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class TeacherSubject
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        
        public Teacher Teacher { get; set; } = null!;
        
        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; } = null!;
        
        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
} 