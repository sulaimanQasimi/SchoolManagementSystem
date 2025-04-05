using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Author { get; set; }
        
        [StringLength(20)]
        public string? ISBN { get; set; }
        
        [StringLength(100)]
        public string? Publisher { get; set; }
        
        public int? PublicationYear { get; set; }
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        
        public BookCategory? Category { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        public int AvailableQuantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }
        
        [StringLength(50)]
        public string? Location { get; set; }
        
        public DateTime AddedDate { get; set; } = DateTime.Now;
        
        public virtual ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();
    }
} 