using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Calendar
{
    public class EventAttendee
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        
        public virtual SchoolEvent Event { get; set; } = null!;
        
        [Required]
        [StringLength(450)]
        public string UserId { get; set; } = string.Empty;
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
        
        public enum AttendanceStatus
        {
            Invited = 1,
            Confirmed = 2,
            Declined = 3,
            Tentative = 4,
            Attended = 5,
            NoShow = 6
        }
        
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Invited;
        
        public DateTime InvitedOn { get; set; } = DateTime.Now;
        
        public DateTime? ResponseDate { get; set; }
        
        [StringLength(500)]
        public string? Comments { get; set; }
        
        public bool IsRequired { get; set; } = false;
    }
} 