using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        
        public ApplicationUser User { get; set; } = null!;
        
        [StringLength(50)]
        public string? Occupation { get; set; }
        
        [StringLength(20)]
        public string? AlternatePhoneNumber { get; set; }
        
        [StringLength(100)]
        public string? Relationship { get; set; } = "Parent";
        
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
} 