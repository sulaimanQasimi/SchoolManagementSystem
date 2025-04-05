using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
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
        
        [StringLength(15)]
        public string? Gender { get; set; }
        
        [StringLength(255)]
        public string? ProfilePicture { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? LastLogin { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
} 