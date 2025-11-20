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
        [HttpPost("schedule/create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateScheduleEntry([FromBody] ScheduleEntryDto scheduleEntryDto)
        {
            try
            {
                var createdScheduleEntry = await _scheduleEntryService.CreateScheduleEntry(scheduleEntryDto);
                return Ok(new { message = "ScheduleEntry added successfully" });
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

        //[HttpPost("schedule/save-group/{groupId}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> SaveScheduleForGroup(int groupId, [FromBody] List<ScheduleEntryDto> entries)
        //{
        //    if (entries == null || !entries.Any())
        //        return BadRequest("No schedule entries provided.");

        //    try
        //    {
        //        var savedEntries = await _scheduleEntryService.SaveScheduleEntriesForGroup(groupId, entries);
        //        return Ok(new { message = "Schedule saved successfully", savedCount = savedEntries.Count() });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($">[ScheduleEntryCtr] Error saving schedule: {ex.Message}");
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



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
    }
}
