namespace SchoolAPI.Dtos
{
    public class ParentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Occupation { get; set; }
        public string? RelationToStudent { get; set; }
        public List<string>? Students { get; set; }
    }
    
    public class ParentCreateDto
    {
        public string UserId { get; set; } = string.Empty;
        public string? Occupation { get; set; }
        public string? RelationToStudent { get; set; }
    }
    
    public class ParentUpdateDto
    {
        public string? Occupation { get; set; }
        public string? RelationToStudent { get; set; }
    }
} 