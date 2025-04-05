using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Transport
{
    public class Route
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string StartingPoint { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string EndingPoint { get; set; } = string.Empty;
        
        public decimal Distance { get; set; }
        
        public TimeSpan? EstimatedTravelTime { get; set; }
        
        public decimal MonthlyFee { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        
        public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
        
        public virtual ICollection<StudentTransport> StudentTransports { get; set; } = new List<StudentTransport>();
    }
} 