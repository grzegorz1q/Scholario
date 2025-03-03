using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Application.Dtos.StudentAttendance;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("student-attendances")]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IStudentAttendanceService _studentAttendanceService;
        public StudentAttendanceController(IStudentAttendanceService studentAttendanceService)
        {
            _studentAttendanceService = studentAttendanceService;
        }
        [HttpPost]
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateStudentAttendance(CreateStudentAttendanceDto studentAttendanceDto)
        {
            try
            {
                await _studentAttendanceService.CreateStudentAttendance(studentAttendanceDto);
                return Ok("StudentAttendance added successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($">[StudentAttendanceCtr] Received null value: {ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[StudentAttendanceCtr] Unhandled exception: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
