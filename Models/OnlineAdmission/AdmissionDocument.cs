using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.OnlineAdmission
{
    public class AdmissionDocument
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("AdmissionApplication")]
        public int ApplicationId { get; set; }
        
        public virtual AdmissionApplication Application { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string DocumentType { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string FilePath { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? OriginalFileName { get; set; }
        
        [StringLength(100)]
        public string? FileExtension { get; set; }
        
        public long FileSize { get; set; }
        
        public DateTime UploadDate { get; set; } = DateTime.Now;
        
        public bool IsVerified { get; set; } = false;
        
        [StringLength(100)]
        public string? VerifiedBy { get; set; }
        
        public DateTime? VerificationDate { get; set; }
        
        [StringLength(500)]
        public string? Remarks { get; set; }
    }
} 