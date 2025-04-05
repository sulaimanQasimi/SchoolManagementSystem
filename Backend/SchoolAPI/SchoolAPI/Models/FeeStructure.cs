using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class FeeStructure
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        
        public Class? Class { get; set; }
        
        [Required]
        [StringLength(20)]
        public string FeeType { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Frequency { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? AcademicYear { get; set; }
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<FeePayment> FeePayments { get; set; } = new List<FeePayment>();
    }
} 