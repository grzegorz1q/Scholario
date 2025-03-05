using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;
using System.Security.Claims;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;
        public StudentController(IStudentService studentService, IGradeService gradeService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
        }

        [HttpPut("group")]
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddOrChangeStudentGroup(AddOrChangeStudentToGroupDto addOrChangeStudentToGroupDto)
        {
            try
            {
                await _studentService.AddOrChangeStudentGroup(addOrChangeStudentToGroupDto);
                return Ok("Student added to group successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[StudentController] Exception: {ex.Message} \n Inner: {ex.InnerException?.Message}");
                return BadRequest($"Unexpected error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
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

        [HttpGet("grade")]
        public async Task<IActionResult> GetStudentGrade()
        {
            try
            {
                // Pobieranie studentId z tokenu JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token.");
                }
                var userId = int.Parse(userIdClaim);
                var userGrade = await _studentService.GetStudentGrade(userId);

                return Ok(userGrade);
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
