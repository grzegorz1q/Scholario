using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("schedule/creates")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateScheduleEntries([FromBody] List<ScheduleEntryDto> entries)
        {
            if (entries == null || entries.Count == 0)
                return BadRequest("No schedule entries provided.");

            var saved = new List<ScheduleEntryDto>();

            try
            {
                foreach (var dto in entries)
                {
                    await _scheduleEntryService.CreateScheduleEntry(dto);
                }

                return Ok(new { message = "Schedule entries saved successfully" });
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
                var createScheduleEntry = await _scheduleEntryService.CreateScheduleEntry(scheduleEntryDto);
                return Ok(new { message = "Schedule entries saved successfully" });
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
        [Authorize(Roles = "Admin,Teacher,Student,Parent")]
        public async Task<IActionResult> GetUserSchedule()
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
                var userSchedule = await _scheduleEntryService.GetUserSchedule(userId);

                return Ok(userSchedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("group/{groupId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetScheduleByGroup(int groupId)
        {
            try
            {
                var schedule = await _scheduleEntryService.GetScheduleByGroupId(groupId);
                return Ok(schedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byDayAndLesson")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetByDayAndLesson([FromQuery] int day, [FromQuery] int lessonNumber)
        {
            var entries = await _scheduleEntryService.GetScheduleByDayAndLesson(day, lessonNumber);
            return Ok(entries);
        }
    }
}
