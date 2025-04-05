using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Transport
{
    public class Route
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(100)]
        public string StartLocation { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string EndLocation { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        
        public Vehicle Vehicle { get; set; } = null!;
        
        public TimeSpan? MorningDepartureTime { get; set; }
        
        public TimeSpan? AfternoonDepartureTime { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Fare { get; set; }
        
        public virtual ICollection<RouteStop> Stops { get; set; } = new List<RouteStop>();
        
        public virtual ICollection<StudentTransport> StudentTransports { get; set; } = new List<StudentTransport>();
    }
} 