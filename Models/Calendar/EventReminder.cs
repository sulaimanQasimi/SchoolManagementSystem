using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Calendar
{
    public class EventReminder
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        
        public virtual SchoolEvent Event { get; set; } = null!;
        
        [StringLength(450)]
        public string? UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
        
        public enum ReminderType
        {
            Email = 1,
            Notification = 2,
            SMS = 3
        }
        
        public ReminderType Type { get; set; } = ReminderType.Notification;
        
        // How many minutes before the event to send the reminder
        public int MinutesBefore { get; set; } = 30;
        
        public bool IsSent { get; set; } = false;
        
        public DateTime? SentAt { get; set; }
        
        [StringLength(500)]
        public string? Message { get; set; }
        
        // Whether this is a system default reminder or user-created
        public bool IsSystemDefault { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}