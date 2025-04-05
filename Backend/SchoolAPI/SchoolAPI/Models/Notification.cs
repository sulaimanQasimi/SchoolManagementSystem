using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;
        
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string ForRole { get; set; } = string.Empty;
        
        [ForeignKey("ForClass")]
        public int? ForClassId { get; set; }
        
        public Class? ForClass { get; set; }
        
        [ForeignKey("ForUser")]
        public string? ForUserId { get; set; }
        
        public ApplicationUser? ForUser { get; set; }
        
        public bool IsRead { get; set; } = false;
        
        public DateTime? ReadAt { get; set; }
    }
} 