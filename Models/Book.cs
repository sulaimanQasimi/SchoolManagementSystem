using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ISBN { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Author { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Publisher { get; set; }
        
        public int? PublicationYear { get; set; }
        
        [StringLength(100)]
        public string? Edition { get; set; }
        
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        public virtual BookCategory Category { get; set; } = null!;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public int TotalCopies { get; set; }
        
        [Required]
        public int AvailableCopies { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [StringLength(200)]
        public string? ShelfLocation { get; set; }
        
        [Required]
        public DateTime AddedDate { get; set; } = DateTime.Now;
        
        [StringLength(200)]
        public string? CoverImage { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(500)]
        public string? Remarks { get; set; }
        
        public virtual ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();
    }
} 