
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.Grade;
using Scholario.Application.Interfaces;

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
        public async Task<IActionResult> AddGradeToStudent(AddOrUpdateGradeToStudentDto addGradeToStudentDto)
        {
            try
            {
                await _gradeService.AddGradeToStudent(addGradeToStudentDto);
                return Ok("Grade added successfully");
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
        public async Task<IActionResult> UpdateStudentGrade(AddOrUpdateGradeToStudentDto updateStudentGrade)
        {
            try
            {
                await _gradeService.UpdateStudentGrade(updateStudentGrade);
                return Ok("Grade updated successfully");
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
