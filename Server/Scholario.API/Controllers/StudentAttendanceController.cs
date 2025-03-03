using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Application.Dtos.StudentAttendance;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;
using System.Security.Claims;

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
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateStudentAttendance(CreateStudentAttendanceDto studentAttendanceDto)
        {
            try
            {
                var teacherIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (teacherIdClaim == null)
                {
                    return Unauthorized("Teacher's ID is missing in the token.");
                }
                var teacherId = int.Parse(teacherIdClaim);
                await _studentAttendanceService.CreateStudentAttendance(studentAttendanceDto, teacherId) ;
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
