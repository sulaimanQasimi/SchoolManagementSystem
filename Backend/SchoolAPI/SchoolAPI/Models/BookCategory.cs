using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
} 