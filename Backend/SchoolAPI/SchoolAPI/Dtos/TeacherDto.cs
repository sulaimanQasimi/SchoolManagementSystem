namespace SchoolAPI.Dtos
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Qualification { get; set; }
        public string? Specialization { get; set; }
        public DateTime JoiningDate { get; set; }
        public int? ClassTeacherOfClassId { get; set; }
        public string? ClassTeacherOfClassName { get; set; }
        public int? ClassTeacherOfSectionId { get; set; }
        public string? ClassTeacherOfSectionName { get; set; }
        public List<TeacherSubjectDto>? Subjects { get; set; }
    }
    
    public class TeacherCreateDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string? Qualification { get; set; }
        public string? Specialization { get; set; }
        public DateTime JoiningDate { get; set; } = DateTime.Now;
        public int? ClassTeacherOfClassId { get; set; }
        public int? ClassTeacherOfSectionId { get; set; }
    }
    
    public class TeacherUpdateDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? Qualification { get; set; }
        public string? Specialization { get; set; }
        public int? ClassTeacherOfClassId { get; set; }
        public int? ClassTeacherOfSectionId { get; set; }
    }
} 