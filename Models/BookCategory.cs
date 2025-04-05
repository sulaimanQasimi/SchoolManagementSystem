using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
} 