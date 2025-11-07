using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.LessonHour;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Application.Interfaces;
using System.Security.Claims;
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

        [HttpGet("schedule/entries")]
        public async Task<IActionResult> GetStudentSchedule()
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
                var userSchedule = await _scheduleEntryService.GetStudentSchedule(userId);

                return Ok(userSchedule);
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
