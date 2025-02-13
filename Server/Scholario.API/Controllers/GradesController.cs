
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos;
using Scholario.Application.Interfaces;
using System.Security.Claims;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("grades")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddGradeToStudent(AddOrUpdateGradeToStudentDto addGradeToStudentDto)
        {
            try
            {
                var teacherIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (teacherIdClaim == null)
                {
                    return Unauthorized("Teacher's ID is missing in the token.");
                }
                var teacherId = int.Parse(teacherIdClaim);
                await _gradeService.AddGradeToStudent(addGradeToStudentDto, teacherId);
                return Ok("Grade added successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($">[GradeCtr] Unauthorized access: {ex.Message}");
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($">[GradeCtr] No grades were found: {ex.Message}");
                return NotFound("There is no grades in the database.");
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

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateStudentGrade(AddOrUpdateGradeToStudentDto updateStudentGrade)
        {
            try
            {
                var teacherIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (teacherIdClaim == null)
                {
                    return Unauthorized("Teacher's ID is missing in the token.");
                }
                var teacherId = int.Parse(teacherIdClaim);
                await _gradeService.UpdateStudentGrade(updateStudentGrade, teacherId);
                return Ok("Grade updated successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($">[GradeCtr] Unauthorized access: {ex.Message}");
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($">[GradeCtr] No grades were found: {ex.Message}");
                return NotFound("There is no grades in the database.");
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
