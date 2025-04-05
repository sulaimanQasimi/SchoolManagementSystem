using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
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
        
        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        public virtual ICollection<ExamResult> Results { get; set; } = new List<ExamResult>();
    }
} 