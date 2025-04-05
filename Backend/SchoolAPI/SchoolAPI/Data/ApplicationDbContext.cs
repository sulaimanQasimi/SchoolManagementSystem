using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using SchoolAPI.Models.Calendar;
using SchoolAPI.Models.Document;
using SchoolAPI.Models.OnlineAdmission;
using SchoolAPI.Models.Transport;

namespace SchoolAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Parent> Parents { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Section> Sections { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<TeacherSubject> TeacherSubjects { get; set; } = null!;
        public DbSet<Attendance> Attendances { get; set; } = null!;
        public DbSet<Exam> Exams { get; set; } = null!;
        public DbSet<ExamResult> ExamResults { get; set; } = null!;
        public DbSet<Timetable> Timetables { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<FeeStructure> FeeStructures { get; set; } = null!;
        public DbSet<FeePayment> FeePayments { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookCategory> BookCategories { get; set; } = null!;
        public DbSet<BookIssue> BookIssues { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Route> Routes { get; set; } = null!;
        public DbSet<RouteStop> RouteStops { get; set; } = null!;
        public DbSet<StudentTransport> StudentTransports { get; set; } = null!;
        public DbSet<AdmissionApplication> AdmissionApplications { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<DocumentCategory> DocumentCategories { get; set; } = null!;
        public DbSet<UserPreference> UserPreferences { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Configure relationships
            builder.Entity<TeacherSubject>()
                .HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeacherSubjects)
                .HasForeignKey(ts => ts.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.Entity<TeacherSubject>()
                .HasOne(ts => ts.Subject)
                .WithMany(s => s.TeacherSubjects)
                .HasForeignKey(ts => ts.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<Student>()
                .HasOne(s => s.Section)
                .WithMany(sec => sec.Students)
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<Teacher>()
                .HasOne(t => t.ClassTeacherOfClass)
                .WithMany(c => c.ClassTeachers)
                .HasForeignKey(t => t.ClassTeacherOfClassId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<Teacher>()
                .HasOne(t => t.ClassTeacherOfSection)
                .WithMany(s => s.ClassTeachers)
                .HasForeignKey(t => t.ClassTeacherOfSectionId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<Section>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Sections)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Subject>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Exam>()
                .HasOne(e => e.Class)
                .WithMany(c => c.Exams)
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<ExamResult>()
                .HasOne(er => er.Exam)
                .WithMany(e => e.Results)
                .HasForeignKey(er => er.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<ExamResult>()
                .HasOne(er => er.Student)
                .WithMany(s => s.ExamResults)
                .HasForeignKey(er => er.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<ExamResult>()
                .HasOne(er => er.Subject)
                .WithMany(s => s.ExamResults)
                .HasForeignKey(er => er.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Attendance>()
                .HasOne(a => a.Teacher)
                .WithMany(t => t.Attendances)
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<FeePayment>()
                .HasOne(fp => fp.Student)
                .WithMany(s => s.FeePayments)
                .HasForeignKey(fp => fp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<FeePayment>()
                .HasOne(fp => fp.FeeStructure)
                .WithMany(fs => fs.FeePayments)
                .HasForeignKey(fp => fp.FeeStructureId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<BookIssue>()
                .HasOne(bi => bi.Book)
                .WithMany(b => b.BookIssues)
                .HasForeignKey(bi => bi.BookId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<BookIssue>()
                .HasOne(bi => bi.Student)
                .WithMany(s => s.BookIssues)
                .HasForeignKey(bi => bi.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<RouteStop>()
                .HasOne(rs => rs.Route)
                .WithMany(r => r.Stops)
                .HasForeignKey(rs => rs.RouteId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<StudentTransport>()
                .HasOne(st => st.Student)
                .WithMany()
                .HasForeignKey(st => st.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<StudentTransport>()
                .HasOne(st => st.Route)
                .WithMany(r => r.StudentTransports)
                .HasForeignKey(st => st.RouteId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<StudentTransport>()
                .HasOne(st => st.RouteStop)
                .WithMany(rs => rs.StudentTransports)
                .HasForeignKey(st => st.RouteStopId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<Route>()
                .HasOne(r => r.Vehicle)
                .WithMany(v => v.Routes)
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Document>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<UserPreference>()
                .HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 