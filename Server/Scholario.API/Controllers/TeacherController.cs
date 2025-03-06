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
        private readonly IGroupService _groupService;
        public TeacherController(ITeacherService teacherService, IStudentService studentService, IGroupService groupService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _groupService = groupService;
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

        [HttpGet("groups")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetLoggedTeacherGroups() //Zwracanie grup, które uczy zalogowany nauczyciel(grupa której jest wychowawcą zwracana jest w GroupController)
        {
            try
            {
                var teacherIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (teacherIdClaim == null)
                {
                    return Unauthorized("Teacher's ID is missing in the token.");
                }
                var teacherId = int.Parse(teacherIdClaim);
                var groups = await _groupService.GetLoggedTeacherGroups(teacherId);
                return Ok(groups);
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Unauthorized access: {ex.Message}");
                return Unauthorized();
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
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($">[TeacherCtr] NotFound exception: {ex.Message}");
                return NotFound($"NotFound error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[TeacherCtr] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }
    }
}
