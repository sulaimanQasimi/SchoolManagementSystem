using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.OnlineAdmission
{
    public class AdmissionApplication
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ApplicationNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [StringLength(15)]
        public string Gender { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Address { get; set; }
        
        [StringLength(15)]
        public string? ContactNumber { get; set; }
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        [Required]
        [StringLength(50)]
        public string ParentName { get; set; } = string.Empty;
        
        [StringLength(15)]
        public string? ParentContact { get; set; }
        
        [StringLength(15)]
        public string? RelationToStudent { get; set; }
        
        [StringLength(100)]
        public string? ParentOccupation { get; set; }
        
        [StringLength(100)]
        public string? PreviousSchool { get; set; }
        
        [StringLength(255)]
        public string? DocumentsPath { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";
        
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        
        public DateTime? ReviewedDate { get; set; }
        
        [StringLength(50)]
        public string? ReviewedBy { get; set; }
        
        [StringLength(255)]
        public string? Remarks { get; set; }
    }
}