using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class BookIssue
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        
        public Book Book { get; set; } = null!;
        
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        
        public Student Student { get; set; } = null!;
        
        [Required]
        public DateTime IssueDate { get; set; } = DateTime.Now;
        
        [Required]
        public DateTime DueDate { get; set; }
        
        public DateTime? ReturnDate { get; set; }
        
        [StringLength(20)]
        public string Status { get; set; } = "Issued";
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FineAmount { get; set; }
        
        [StringLength(50)]
        public string IssuedBy { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? ReturnedTo { get; set; }
        
        [StringLength(255)]
        public string? Remarks { get; set; }
    }
} 