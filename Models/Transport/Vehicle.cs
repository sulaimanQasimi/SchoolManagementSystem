using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Transport
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string RegistrationNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string VehicleType { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Manufacturer { get; set; }
        
        public int? ManufacturingYear { get; set; }
        
        public int SeatingCapacity { get; set; }
        
        [StringLength(100)]
        public string? Color { get; set; }
        
        [StringLength(100)]
        public string? FuelType { get; set; }
        
        public DateTime? InsuranceExpiryDate { get; set; }
        
        public DateTime? PermitExpiryDate { get; set; }
        
        public DateTime? PollutionCertificateExpiryDate { get; set; }
        
        public DateTime? LastServiceDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(500)]
        public string? Remarks { get; set; }
        
        [ForeignKey("Driver")]
        public int? DriverId { get; set; }
        
        public virtual Driver? Driver { get; set; }
        
        [Required]
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        
        public virtual Route Route { get; set; } = null!;
        
        public virtual ICollection<StudentTransport> StudentTransports { get; set; } = new List<StudentTransport>();
    }
} 