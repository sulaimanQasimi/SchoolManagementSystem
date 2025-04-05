using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Dtos;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StudentDto>>>> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Class)
                .Include(s => s.Section)
                .Include(s => s.Parent)
                    .ThenInclude(p => p != null ? p.User : null)
                .ToListAsync();

            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(ApiResponse<IEnumerable<StudentDto>>.SuccessResponse(studentDtos));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher,Student,Parent")]
        public async Task<ActionResult<ApiResponse<StudentDto>>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Class)
                .Include(s => s.Section)
                .Include(s => s.Parent)
                    .ThenInclude(p => p != null ? p.User : null)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound(ApiResponse<StudentDto>.FailureResponse("Student not found"));
            }

            // Check if the current user has permissions to view this student
            if (User.IsInRole("Student"))
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (student.UserId != currentUserId)
                {
                    return Forbid();
                }
            }
            else if (User.IsInRole("Parent"))
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var parent = await _context.Parents.FirstOrDefaultAsync(p => p.UserId == currentUserId);
                if (parent == null || student.ParentId != parent.Id)
                {
                    return Forbid();
                }
            }

            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(ApiResponse<StudentDto>.SuccessResponse(studentDto));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<StudentDto>>> CreateStudent(StudentCreateDto studentCreateDto)
        {
            // Validate that user exists
            var user = await _context.Users.FindAsync(studentCreateDto.UserId);
            if (user == null)
            {
                return BadRequest(ApiResponse<StudentDto>.FailureResponse("User does not exist"));
            }

            // Validate parent if provided
            if (studentCreateDto.ParentId.HasValue)
            {
                var parent = await _context.Parents.FindAsync(studentCreateDto.ParentId.Value);
                if (parent == null)
                {
                    return BadRequest(ApiResponse<StudentDto>.FailureResponse("Parent does not exist"));
                }
            }

            // Validate class if provided
            if (studentCreateDto.ClassId.HasValue)
            {
                var classObj = await _context.Classes.FindAsync(studentCreateDto.ClassId.Value);
                if (classObj == null)
                {
                    return BadRequest(ApiResponse<StudentDto>.FailureResponse("Class does not exist"));
                }
            }

            // Validate section if provided
            if (studentCreateDto.SectionId.HasValue)
            {
                var section = await _context.Sections.FindAsync(studentCreateDto.SectionId.Value);
                if (section == null)
                {
                    return BadRequest(ApiResponse<StudentDto>.FailureResponse("Section does not exist"));
                }

                // Ensure section belongs to the selected class
                if (studentCreateDto.ClassId.HasValue && section.ClassId != studentCreateDto.ClassId.Value)
                {
                    return BadRequest(ApiResponse<StudentDto>.FailureResponse("Section does not belong to the selected class"));
                }
            }

            var student = _mapper.Map<Student>(studentCreateDto);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var createdStudent = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Class)
                .Include(s => s.Section)
                .Include(s => s.Parent)
                    .ThenInclude(p => p != null ? p.User : null)
                .FirstOrDefaultAsync(s => s.Id == student.Id);

            var studentDto = _mapper.Map<StudentDto>(createdStudent);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, ApiResponse<StudentDto>.SuccessResponse(studentDto, "Student created successfully"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateDto studentUpdateDto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(ApiResponse<object>.FailureResponse("Student not found"));
            }

            // Validate class if provided
            if (studentUpdateDto.ClassId.HasValue)
            {
                var classObj = await _context.Classes.FindAsync(studentUpdateDto.ClassId.Value);
                if (classObj == null)
                {
                    return BadRequest(ApiResponse<object>.FailureResponse("Class does not exist"));
                }
            }

            // Validate section if provided
            if (studentUpdateDto.SectionId.HasValue)
            {
                var section = await _context.Sections.FindAsync(studentUpdateDto.SectionId.Value);
                if (section == null)
                {
                    return BadRequest(ApiResponse<object>.FailureResponse("Section does not exist"));
                }

                // Ensure section belongs to the selected class
                if (studentUpdateDto.ClassId.HasValue && section.ClassId != studentUpdateDto.ClassId.Value)
                {
                    return BadRequest(ApiResponse<object>.FailureResponse("Section does not belong to the selected class"));
                }
            }

            // Validate parent if provided
            if (studentUpdateDto.ParentId.HasValue)
            {
                var parent = await _context.Parents.FindAsync(studentUpdateDto.ParentId.Value);
                if (parent == null)
                {
                    return BadRequest(ApiResponse<object>.FailureResponse("Parent does not exist"));
                }
            }

            // Update student properties
            student.AdmissionNumber = studentUpdateDto.AdmissionNumber;
            student.ClassId = studentUpdateDto.ClassId;
            student.SectionId = studentUpdateDto.SectionId;
            student.ParentId = studentUpdateDto.ParentId;

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentExists(id))
                {
                    return NotFound(ApiResponse<object>.FailureResponse("Student not found"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(ApiResponse<object>.FailureResponse("Student not found"));
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(new { }, "Student deleted successfully"));
        }

        private async Task<bool> StudentExists(int id)
        {
            return await _context.Students.AnyAsync(e => e.Id == id);
        }
    }
} 