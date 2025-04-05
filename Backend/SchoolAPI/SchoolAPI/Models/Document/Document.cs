using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Document
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(255)]
        public string FilePath { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? FileType { get; set; }
        
        public long? FileSize { get; set; }
        
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        
        public DocumentCategory? Category { get; set; }
        
        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        
        public Class? Class { get; set; }
        
        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        
        public Subject? Subject { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ForRole { get; set; } = "All";
        
        [Required]
        [ForeignKey("UploadedBy")]
        public string UploadedById { get; set; } = string.Empty;
        
        public ApplicationUser UploadedBy { get; set; } = null!;
        
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        
        public int DownloadCount { get; set; } = 0;
        
        public bool IsPublic { get; set; } = true;
    }
} 