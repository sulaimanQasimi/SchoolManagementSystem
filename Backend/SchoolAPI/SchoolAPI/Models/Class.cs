using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
        
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        
        public virtual ICollection<Teacher> ClassTeachers { get; set; } = new List<Teacher>();
        
        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
} 