
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos;
using Scholario.Application.Interfaces;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpPost] 
        public async Task<IActionResult> AddGradeToStudent(AddGradeToStudentDto addGradeToStudentDto)
        {
            try
            {
                await _gradeService.AddGradeToStudent(addGradeToStudentDto);
                return Ok("Grade added successfully");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($">[GradeCtr] No projects were found: {ex.Message}");
                return NotFound("There is no projects in the database.");
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
