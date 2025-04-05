using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class FeePayment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ReceiptNumber { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        
        public Student Student { get; set; } = null!;
        
        [Required]
        [ForeignKey("FeeStructure")]
        public int FeeStructureId { get; set; }
        
        public FeeStructure FeeStructure { get; set; } = null!;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }
        
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? TransactionId { get; set; }
        
        [StringLength(20)]
        public string? Status { get; set; } = "Completed";
        
        [StringLength(50)]
        public string ReceivedBy { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Remarks { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Fine { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Discount { get; set; }
    }
} 