using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Document
{
    public class DocumentAccess
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        
        public virtual SchoolDocument Document { get; set; } = null!;
        
        [Required]
        [StringLength(450)]
        public string UserId { get; set; } = string.Empty;
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
        
        public bool CanView { get; set; } = true;
        
        public bool CanEdit { get; set; } = false;
        
        public bool CanDelete { get; set; } = false;
        
        public bool CanShare { get; set; } = false;
        
        public DateTime GrantedOn { get; set; } = DateTime.Now;
        
        [StringLength(450)]
        public string? GrantedBy { get; set; }
        
        [ForeignKey("GrantedBy")]
        public virtual ApplicationUser? Grantor { get; set; }
        
        public DateTime? LastAccessed { get; set; }
        
        public int AccessCount { get; set; } = 0;
    }
} 