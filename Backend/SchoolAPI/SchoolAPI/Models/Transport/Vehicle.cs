using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.Transport
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string VehicleNumber { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Type { get; set; }
        
        [StringLength(100)]
        public string? Model { get; set; }
        
        public int? Capacity { get; set; }
        
        [StringLength(100)]
        public string? DriverName { get; set; }
        
        [StringLength(15)]
        public string? DriverContact { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
    }
} 