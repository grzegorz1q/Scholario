using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.LessonHour;
using Scholario.Application.Interfaces;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("lesson-hours")]
    public class LessonHourController : ControllerBase
    {
        private readonly ILessonHourService _lessonHourService;
        public LessonHourController(ILessonHourService lessonHourService)
        {
            _lessonHourService = lessonHourService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLessonHour([FromBody] LessonHourDto lessonHourDto)
        {
            try
            {
                var createdLessonHour = await _lessonHourService.CreateLessonHour(lessonHourDto);
                return Ok("LessoHour added successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($">[ScheduleEntryCtr] Received null value: {ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[ScheduleEntryCtr] Unhandled exception: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLessonHours()
        {
            try
            {
                var lessonHours = await _lessonHourService.GetAllLessonHours();
                return Ok(lessonHours);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
