using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class BookIssue
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        
        public virtual Book Book { get; set; } = null!;
        
        [Required]
        [ForeignKey("IssuedTo")]
        public string IssuedToId { get; set; } = string.Empty;
        
        public virtual ApplicationUser IssuedTo { get; set; } = null!;
        
        [Required]
        public DateTime IssueDate { get; set; } = DateTime.Now;
        
        [Required]
        public DateTime DueDate { get; set; }
        
        public DateTime? ReturnDate { get; set; }
        
        public decimal? LateFee { get; set; }
        
        [Required]
        public IssueStatus Status { get; set; } = IssueStatus.Issued;
        
        [Required]
        [ForeignKey("IssuedBy")]
        public string IssuedById { get; set; } = string.Empty;
        
        public virtual ApplicationUser IssuedBy { get; set; } = null!;
        
        [ForeignKey("ReturnedTo")]
        public string? ReturnedToId { get; set; }
        
        public virtual ApplicationUser? ReturnedTo { get; set; }
        
        [StringLength(500)]
        public string? Remarks { get; set; }
        
        [NotMapped]
        public int DaysOverdue
        {
            get
            {
                if (ReturnDate.HasValue || Status == IssueStatus.Returned)
                    return 0;
                    
                var today = DateTime.Now.Date;
                return today > DueDate.Date ? (today - DueDate.Date).Days : 0;
            }
        }
    }
    
    public enum IssueStatus
    {
        Issued = 1,
        Returned = 2,
        Overdue = 3,
        Lost = 4,
        Damaged = 5
    }
} 