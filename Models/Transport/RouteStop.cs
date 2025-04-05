using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Transport
{
    public class RouteStop
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Address { get; set; }
        
        public int StopOrder { get; set; }
        
        public TimeSpan? PickupTime { get; set; }
        
        public TimeSpan? DropTime { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        
        public virtual Route Route { get; set; } = null!;
        
        public virtual ICollection<StudentTransport> StudentTransports { get; set; } = new List<StudentTransport>();
    }
} 