using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Transport
{
    public class StudentTransport
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        
        public Student Student { get; set; } = null!;
        
        [Required]
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        
        public Route Route { get; set; } = null!;
        
        [ForeignKey("RouteStop")]
        public int? RouteStopId { get; set; }
        
        public RouteStop? RouteStop { get; set; }
        
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FeeAmount { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(255)]
        public string? Remarks { get; set; }
    }
}