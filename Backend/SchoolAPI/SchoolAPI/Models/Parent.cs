using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        
        public ApplicationUser User { get; set; } = null!;
        
        [StringLength(100)]
        public string? Occupation { get; set; }
        
        [StringLength(15)]
        public string? RelationToStudent { get; set; }
        
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
} 