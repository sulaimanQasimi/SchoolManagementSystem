using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.Document
{
    public class DocumentCategory
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }
} 