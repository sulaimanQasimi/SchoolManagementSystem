using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SchoolManagementSystem.Models.Document
{
    public class SchoolDocument
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(255)]
        public string FilePath { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? OriginalFileName { get; set; }
        
        [StringLength(50)]
        public string? FileExtension { get; set; }
        
        public long FileSize { get; set; }
        
        public enum DocumentType
        {
            Circular = 1,
            Notice = 2,
            Assignment = 3,
            StudyMaterial = 4,
            Syllabus = 5,
            TimeTable = 6,
            Report = 7,
            Other = 8
        }
        
        public DocumentType Type { get; set; } = DocumentType.Other;
        
        public enum AccessLevel
        {
            Public = 1,        // Accessible to all users
            Staff = 2,         // Only teachers and admin
            Students = 3,      // Only students
            Parents = 4,       // Only parents
            Class = 5,         // Specific class only
            Section = 6,       // Specific section only
            Individual = 7     // Specific users only
        }
        
        public AccessLevel VisibilityLevel { get; set; } = AccessLevel.Public;
        
        // Target audience fields (based on AccessLevel)
        public int? ClassId { get; set; }
        
        [ForeignKey("ClassId")]
        public virtual Class? Class { get; set; }
        
        public int? SectionId { get; set; }
        
        [ForeignKey("SectionId")]
        public virtual Section? Section { get; set; }
        
        public int? SubjectId { get; set; }
        
        [ForeignKey("SubjectId")]
        public virtual Subject? Subject { get; set; }
        
        [Required]
        [StringLength(450)]
        public string UploadedBy { get; set; } = string.Empty;
        
        [ForeignKey("UploadedBy")]
        public virtual ApplicationUser Uploader { get; set; } = null!;
        
        public DateTime UploadDate { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(500)]
        public string? Tags { get; set; }
        
        public virtual ICollection<DocumentAccess> AccessList { get; set; } = new List<DocumentAccess>();
    }
}