using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Transport
{
    public class StudentTransport
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        
        public virtual Student Student { get; set; } = null!;
        
        [Required]
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        
        public virtual Route Route { get; set; } = null!;
        
        [ForeignKey("RouteStop")]
        public int? RouteStopId { get; set; }
        
        public virtual RouteStop? RouteStop { get; set; }
        
        [ForeignKey("Vehicle")]
        public int? VehicleId { get; set; }
        
        public virtual Vehicle? Vehicle { get; set; }
        
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        
        public DateTime? EndDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(500)]
        public string? Remarks { get; set; }
        
        public enum TransportType
        {
            PickupOnly = 1,
            DropOnly = 2,
            Both = 3
        }
        
        public TransportType Type { get; set; } = TransportType.Both;
    }
} 