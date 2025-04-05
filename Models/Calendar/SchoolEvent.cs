using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Calendar
{
    public class SchoolEvent
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsAllDay { get; set; } = false;
        
        [StringLength(100)]
        public string? Location { get; set; }
        
        public enum EventType
        {
            Holiday = 1,
            Exam = 2,
            Meeting = 3,
            Sports = 4,
            Cultural = 5,
            Academic = 6,
            Other = 7
        }
        
        public EventType Type { get; set; } = EventType.Other;
        
        [StringLength(30)]
        public string? ColorCode { get; set; }
        
        [StringLength(450)]
        public string? CreatedBy { get; set; }
        
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser? Creator { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsPublic { get; set; } = true;
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(500)]
        public string? Remarks { get; set; }
        
        // For targeting specific classes, sections, etc.
        public int? ClassId { get; set; }
        
        [ForeignKey("ClassId")]
        public virtual Class? Class { get; set; }
        
        public int? SectionId { get; set; }
        
        [ForeignKey("SectionId")]
        public virtual Section? Section { get; set; }
        
        public virtual ICollection<EventAttendee> Attendees { get; set; } = new List<EventAttendee>();
        
        public virtual ICollection<EventReminder> Reminders { get; set; } = new List<EventReminder>();
    }
} 