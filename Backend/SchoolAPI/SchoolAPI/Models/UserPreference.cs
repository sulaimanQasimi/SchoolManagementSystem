using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models
{
    public class UserPreference
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        
        public ApplicationUser User { get; set; } = null!;
        
        [Required]
        public bool DarkMode { get; set; } = false;
        
        [StringLength(10)]
        public string? ThemeColor { get; set; } = "blue";
        
        [Required]
        [StringLength(10)]
        public string Language { get; set; } = "en";
        
        public bool EmailNotifications { get; set; } = true;
        
        public bool PushNotifications { get; set; } = true;
        
        public DateTime? LastUpdated { get; set; } = DateTime.Now;
        
        [StringLength(255)]
        public string? Preferences { get; set; }
    }
} 