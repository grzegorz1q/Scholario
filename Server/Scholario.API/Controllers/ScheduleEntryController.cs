using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;
namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("schedule-entries")]
    public class ScheduleEntryController : ControllerBase
    {
        private readonly IScheduleEntryService _scheduleEntryService;

        public ScheduleEntryController(IScheduleEntryService scheduleEntriesService)
        {
            _scheduleEntryService = scheduleEntriesService;
        }

        [HttpPost("hour/create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLessonHour([FromBody] LessonHourDto lessonHourDto)
        {
            try
            {
                var createdLessonHour = await _scheduleEntryService.CreateLessonHour(lessonHourDto);
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

        [HttpPost("schedule/create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateScheduleEntry([FromBody] ScheduleEntryDto scheduleEntryDto)
        {
            try
            {
                var createdScheduleEntry = await _scheduleEntryService.CreateScheduleEntry(scheduleEntryDto);
                return Ok("ScheduleEntry added successfully");
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
    }
}
