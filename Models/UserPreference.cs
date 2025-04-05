using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class UserPreference
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(450)]
        public string UserId { get; set; } = string.Empty;
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
        
        public enum ThemeMode
        {
            Light = 1,
            Dark = 2,
            System = 3
        }
        
        public ThemeMode Theme { get; set; } = ThemeMode.Light;
        
        [StringLength(10)]
        public string Language { get; set; } = "en"; // Default to English
        
        public bool EnableNotifications { get; set; } = true;
        
        public bool EmailNotifications { get; set; } = true;
        
        public bool SmsNotifications { get; set; } = false;
        
        [StringLength(50)]
        public string? TimeZone { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
} 