using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos;
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

        [HttpPost("students")]
        public async Task<IActionResult> GetStudentById(ReadStudentDto readStudentDto)
        {
            try
            {
                var student = await _teacherService.GetStudentById(readStudentDto);

                if (student == null)
                {
                    return NotFound("Student not found");
                }
                return Ok(student);
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
    }
}
