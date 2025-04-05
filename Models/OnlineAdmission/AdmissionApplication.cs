using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.OnlineAdmission
{
    public class AdmissionApplication
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ApplicantName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(15)]
        public string MobileNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string FatherName { get; set; } = string.Empty;
        
        [StringLength(15)]
        public string? FatherMobile { get; set; }
        
        [StringLength(100)]
        public string? FatherOccupation { get; set; }
        
        [StringLength(100)]
        public string? MotherName { get; set; }
        
        [StringLength(15)]
        public string? MotherMobile { get; set; }
        
        [StringLength(100)]
        public string? MotherOccupation { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? City { get; set; }
        
        [StringLength(100)]
        public string? State { get; set; }
        
        [StringLength(20)]
        public string? ZipCode { get; set; }
        
        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public virtual Class Class { get; set; } = null!;
        
        [StringLength(100)]
        public string? PreviousSchool { get; set; }
        
        [StringLength(500)]
        public string? AcademicHistory { get; set; }
        
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        
        public enum ApplicationStatus
        {
            Pending = 1,
            UnderReview = 2,
            DocumentsRequired = 3,
            InterviewScheduled = 4,
            Approved = 5,
            Rejected = 6,
            Enrolled = 7,
            Cancelled = 8
        }
        
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        
        [StringLength(500)]
        public string? AdminRemarks { get; set; }
        
        [StringLength(255)]
        public string? ProfilePhotoPath { get; set; }
        
        [StringLength(255)]
        public string? DocumentsPath { get; set; }
        
        public DateTime? InterviewDate { get; set; }
        
        [StringLength(50)]
        public string? ApplicationNumber { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        
        [StringLength(100)]
        public string? ReviewedBy { get; set; }
        
        public DateTime? ReviewDate { get; set; }
        
        public virtual ICollection<AdmissionDocument> Documents { get; set; } = new List<AdmissionDocument>();
    }
} 