using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.Message;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Dtos.Teacher;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;
using System.Security.Claims;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        public TeacherController(ITeacherService teacherService, IStudentService studentService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
        }
        [HttpPost("messages")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddMessageOrNoteToStudent(AddMessageOrNoteToStudentDto addNoteToStudentDto)
        {
            try
            {
                await _teacherService.AddMessageOrNoteToStudent(addNoteToStudentDto);
                return Ok("Note added successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($">[GradeCtr] Received null value: {ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[GradeCtr] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }

        [HttpPut("group")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddOrChangeTeacherToGroup(AddOrChangeTeacherToGroupDto addTeacherToGroupDto)
        {
            try
            {
                await _teacherService.AddOrChangeTeacherToGroup(addTeacherToGroupDto);
                return Ok("Teacher added to group successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[GradeCtr] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }

        [HttpGet("subjects/{subjectId}/groups/{groupId}/students")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetStudents([FromRoute] int groupId, [FromRoute] int subjectId)
        {
            try
            {
                var teacherIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (teacherIdClaim == null)
                {
                    return Unauthorized("Teacher's ID is missing in the token.");
                }
                var teacherId = int.Parse(teacherIdClaim);

                var students = await _studentService.GetStudentsByGroupAndSubject(groupId, subjectId, teacherId);
                return Ok(students);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Unauthorized access: {ex.Message}");
                return Forbid();
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[TeacherCtr] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }
    }
}
