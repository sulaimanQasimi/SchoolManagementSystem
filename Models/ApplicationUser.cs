using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Address { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(100)]
        public string? ProfilePicture { get; set; }
        
        public DateTime JoinDate { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
    }
} 