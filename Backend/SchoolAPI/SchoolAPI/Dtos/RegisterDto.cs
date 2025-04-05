using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
        
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        
        [StringLength(100)]
        public string? Address { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(15)]
        public string? Gender { get; set; }
        
        public string Role { get; set; } = "Student";
    }
} 