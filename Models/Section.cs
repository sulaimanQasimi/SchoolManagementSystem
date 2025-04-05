using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Name { get; set; } = string.Empty;
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        public Class Class { get; set; } = null!;
        
        public int? Capacity { get; set; }
        
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        
        public virtual ICollection<Teacher> ClassTeachers { get; set; } = new List<Teacher>();
        
        public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
    }
} 