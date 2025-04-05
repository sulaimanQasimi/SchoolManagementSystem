namespace SchoolAPI.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string AdmissionNumber { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
        public int? ClassId { get; set; }
        public string? ClassName { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
    }
    
    public class StudentCreateDto
    {
        public string AdmissionNumber { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public int? ParentId { get; set; }
    }
    
    public class StudentUpdateDto
    {
        public string AdmissionNumber { get; set; } = string.Empty;
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? ParentId { get; set; }
    }
} 