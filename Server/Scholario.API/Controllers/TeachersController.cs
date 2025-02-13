using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.Message;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Dtos.Teacher;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
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

        [HttpPut("tgroup")]
        //[Authorize(Roles = "Teacher")]
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

        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _teacherService.GetStudentById(id);

                if (student == null)
                {
                    return NotFound("Student not found");
                }
                return Ok(student);
            }
            catch (ArgumentOutOfRangeException ex)
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
    }
}
