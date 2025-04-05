using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
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
        
        public virtual Student Student { get; set; } = null!;
        
        [Required]
        [ForeignKey("FeeStructure")]
        public int FeeStructureId { get; set; }
        
        public virtual FeeStructure FeeStructure { get; set; } = null!;
        
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        [Required]
        public decimal AmountPaid { get; set; }
        
        public decimal? LateFee { get; set; }
        
        public decimal? Discount { get; set; }
        
        [NotMapped]
        public decimal TotalAmount => AmountPaid + (LateFee ?? 0) - (Discount ?? 0);
        
        [Required]
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        
        [StringLength(100)]
        public string? TransactionReference { get; set; }
        
        [StringLength(100)]
        public string? PaymentPeriod { get; set; }
        
        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Paid;
        
        [StringLength(500)]
        public string? Remarks { get; set; }
        
        [Required]
        [ForeignKey("CollectedBy")]
        public string CollectedById { get; set; } = string.Empty;
        
        public virtual ApplicationUser CollectedBy { get; set; } = null!;
    }
    
    public enum PaymentMethod
    {
        Cash = 1,
        CreditCard = 2,
        DebitCard = 3,
        BankTransfer = 4,
        Check = 5,
        OnlinePayment = 6
    }
    
    public enum PaymentStatus
    {
        Pending = 1,
        Paid = 2,
        Partial = 3,
        Failed = 4,
        Refunded = 5
    }
} 