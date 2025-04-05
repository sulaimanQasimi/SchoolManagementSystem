namespace SchoolAPI.Dtos
{
    public class TeacherSubjectDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public DateTime AssignedDate { get; set; }
    }
    
    public class TeacherSubjectCreateDto
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
} 