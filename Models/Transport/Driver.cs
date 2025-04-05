using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.Transport
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string LicenseNumber { get; set; } = string.Empty;
        
        public DateTime LicenseExpiryDate { get; set; }
        
        [StringLength(15)]
        public string? ContactNumber { get; set; }
        
        [StringLength(200)]
        public string? Address { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public DateTime JoiningDate { get; set; }
        
        [StringLength(100)]
        public string? EmergencyContactName { get; set; }
        
        [StringLength(15)]
        public string? EmergencyContactNumber { get; set; }
        
        [StringLength(500)]
        public string? Remarks { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
} 