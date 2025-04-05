using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Calendar
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsAllDay { get; set; }
        
        [StringLength(20)]
        public string? Type { get; set; }
        
        [StringLength(20)]
        public string? Color { get; set; }
        
        [ForeignKey("Class")]
        public int? ForClassId { get; set; }
        
        public Class? ForClass { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ForRole { get; set; } = "All";
        
        [StringLength(50)]
        public string CreatedBy { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 