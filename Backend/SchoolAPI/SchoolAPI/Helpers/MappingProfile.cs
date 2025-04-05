using AutoMapper;
using SchoolAPI.Dtos;
using SchoolAPI.Models;
using SchoolAPI.Models.Calendar;
using SchoolAPI.Models.Document;
using SchoolAPI.Models.OnlineAdmission;
using SchoolAPI.Models.Transport;

namespace SchoolAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User and Identity mappings
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<RegisterDto, ApplicationUser>();
            
            // Student mappings
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.Name : null))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section != null ? src.Section.Name : null))
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? $"{src.Parent.User.FirstName} {src.Parent.User.LastName}" : null));
            CreateMap<StudentCreateDto, Student>();
            
            // Teacher mappings
            CreateMap<Teacher, TeacherDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture))
                .ForMember(dest => dest.ClassTeacherOfClassName, opt => opt.MapFrom(src => src.ClassTeacherOfClass != null ? src.ClassTeacherOfClass.Name : null))
                .ForMember(dest => dest.ClassTeacherOfSectionName, opt => opt.MapFrom(src => src.ClassTeacherOfSection != null ? src.ClassTeacherOfSection.Name : null));
            CreateMap<TeacherCreateDto, Teacher>();
            
            // Parent mappings
            CreateMap<Parent, ParentDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students.Select(s => $"{s.User.FirstName} {s.User.LastName}")));
            CreateMap<ParentCreateDto, Parent>();
            
            // Class mappings
            CreateMap<Class, ClassDto>();
            CreateMap<ClassCreateDto, Class>();
            
            // Section mappings
            CreateMap<Section, SectionDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));
            CreateMap<SectionCreateDto, Section>();
            
            // Subject mappings
            CreateMap<Subject, SubjectDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));
            CreateMap<SubjectCreateDto, Subject>();
            
            // TeacherSubject mappings
            CreateMap<TeacherSubject, TeacherSubjectDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => $"{src.Teacher.User.FirstName} {src.Teacher.User.LastName}"))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Subject.Class.Name));
            CreateMap<TeacherSubjectCreateDto, TeacherSubject>();
            
            // Attendance mappings
            CreateMap<Attendance, AttendanceDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student != null ? $"{src.Student.User.FirstName} {src.Student.User.LastName}" : null))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? $"{src.Teacher.User.FirstName} {src.Teacher.User.LastName}" : null));
            CreateMap<AttendanceCreateDto, Attendance>();
            
            // Exam mappings
            CreateMap<Exam, ExamDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));
            CreateMap<ExamCreateDto, Exam>();
            
            // ExamResult mappings
            CreateMap<ExamResult, ExamResultDto>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.User.FirstName} {src.Student.User.LastName}"))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));
            CreateMap<ExamResultCreateDto, ExamResult>();
            
            // Timetable mappings
            CreateMap<Timetable, TimetableDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.Name))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => $"{src.Teacher.User.FirstName} {src.Teacher.User.LastName}"))
                .ForMember(dest => dest.DayName, opt => opt.MapFrom(src => GetDayName(src.DayOfWeek)));
            CreateMap<TimetableCreateDto, Timetable>();
            
            // Notification mappings
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.ForClassName, opt => opt.MapFrom(src => src.ForClass != null ? src.ForClass.Name : null))
                .ForMember(dest => dest.ForUserName, opt => opt.MapFrom(src => src.ForUser != null ? $"{src.ForUser.FirstName} {src.ForUser.LastName}" : null));
            CreateMap<NotificationCreateDto, Notification>();
            
            // Fee Structure mappings
            CreateMap<FeeStructure, FeeStructureDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.Name : null));
            CreateMap<FeeStructureCreateDto, FeeStructure>();
            
            // Fee Payment mappings
            CreateMap<FeePayment, FeePaymentDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.User.FirstName} {src.Student.User.LastName}"))
                .ForMember(dest => dest.FeeName, opt => opt.MapFrom(src => src.FeeStructure.Name));
            CreateMap<FeePaymentCreateDto, FeePayment>();
            
            // Book Category mappings
            CreateMap<BookCategory, BookCategoryDto>();
            CreateMap<BookCategoryCreateDto, BookCategory>();
            
            // Book mappings
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
            CreateMap<BookCreateDto, Book>();
            
            // Book Issue mappings
            CreateMap<BookIssue, BookIssueDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.User.FirstName} {src.Student.User.LastName}"));
            CreateMap<BookIssueCreateDto, BookIssue>();
            
            // Vehicle mappings
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleCreateDto, Vehicle>();
            
            // Route mappings
            CreateMap<Route, RouteDto>()
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.Vehicle.VehicleNumber));
            CreateMap<RouteCreateDto, Route>();
            
            // RouteStop mappings
            CreateMap<RouteStop, RouteStopDto>()
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(src => src.Route.Name));
            CreateMap<RouteStopCreateDto, RouteStop>();
            
            // StudentTransport mappings
            CreateMap<StudentTransport, StudentTransportDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.User.FirstName} {src.Student.User.LastName}"))
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(src => src.Route.Name))
                .ForMember(dest => dest.StopName, opt => opt.MapFrom(src => src.RouteStop != null ? src.RouteStop.Name : null));
            CreateMap<StudentTransportCreateDto, StudentTransport>();
            
            // Admission Application mappings
            CreateMap<AdmissionApplication, AdmissionApplicationDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));
            CreateMap<AdmissionApplicationCreateDto, AdmissionApplication>();
            
            // Event mappings
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.ForClassName, opt => opt.MapFrom(src => src.ForClass != null ? src.ForClass.Name : null));
            CreateMap<EventCreateDto, Event>();
            
            // Document Category mappings
            CreateMap<DocumentCategory, DocumentCategoryDto>();
            CreateMap<DocumentCategoryCreateDto, DocumentCategory>();
            
            // Document mappings
            CreateMap<Document, DocumentDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.Name : null))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject != null ? src.Subject.Name : null))
                .ForMember(dest => dest.UploadedByName, opt => opt.MapFrom(src => $"{src.UploadedBy.FirstName} {src.UploadedBy.LastName}"));
            CreateMap<DocumentCreateDto, Document>();
            
            // User Preference mappings
            CreateMap<UserPreference, UserPreferenceDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
            CreateMap<UserPreferenceCreateDto, UserPreference>();
        }
        
        private string GetDayName(int dayOfWeek)
        {
            return dayOfWeek switch
            {
                1 => "Monday",
                2 => "Tuesday",
                3 => "Wednesday",
                4 => "Thursday",
                5 => "Friday",
                6 => "Saturday",
                7 => "Sunday",
                _ => "Unknown"
            };
        }
    }
} 