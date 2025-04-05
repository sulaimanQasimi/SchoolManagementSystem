using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class FeeStructure
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public virtual Class Class { get; set; } = null!;
        
        [Required]
        public decimal TuitionFee { get; set; }
        
        public decimal ExamFee { get; set; }
        
        public decimal LibraryFee { get; set; }
        
        public decimal TransportFee { get; set; }
        
        public decimal MiscellaneousFee { get; set; }
        
        [Required]
        public FeeInterval Interval { get; set; } = FeeInterval.Monthly;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [Required]
        public DateTime EffectiveFrom { get; set; } = DateTime.Now;
        
        public DateTime? EffectiveTo { get; set; }
        
        [NotMapped]
        public decimal TotalFee => TuitionFee + ExamFee + LibraryFee + TransportFee + MiscellaneousFee;
        
        public virtual ICollection<FeePayment> Payments { get; set; } = new List<FeePayment>();
    }
    
    public enum FeeInterval
    {
        Monthly = 1,
        Quarterly = 2,
        HalfYearly = 3,
        Yearly = 4
    }
} 