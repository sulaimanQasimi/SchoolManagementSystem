using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Message { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("CreatedBy")]
        public string? CreatedById { get; set; }
        
        public ApplicationUser? CreatedBy { get; set; }
        
        public NotificationType Type { get; set; }
        
        [StringLength(100)]
        public string? TargetUserRole { get; set; }
        
        [ForeignKey("TargetUser")]
        public string? TargetUserId { get; set; }
        
        public ApplicationUser? TargetUser { get; set; }
        
        [ForeignKey("TargetClass")]
        public int? TargetClassId { get; set; }
        
        public Class? TargetClass { get; set; }
        
        public bool IsRead { get; set; } = false;
    }
    
    public enum NotificationType
    {
        General,
        Specific,
        Announcement,
        Message
    }
} 