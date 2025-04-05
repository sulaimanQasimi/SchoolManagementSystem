using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Transport
{
    public class RouteStop
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        
        public Route Route { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        public int SequenceNumber { get; set; }
        
        public TimeSpan? MorningTime { get; set; }
        
        public TimeSpan? AfternoonTime { get; set; }
        
        public virtual ICollection<StudentTransport> StudentTransports { get; set; } = new List<StudentTransport>();
    }
} 