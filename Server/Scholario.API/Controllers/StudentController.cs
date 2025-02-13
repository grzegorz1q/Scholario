using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("teachers")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPut("group")]
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddOrChangeStudentGroup(AddOrChangeStudentToGroupDto addOrChangeStudentToGroupDto)
        {
            try
            {
                await _studentService.AddOrChangeStudentGroup(addOrChangeStudentToGroupDto);
                return Ok("Student added to group successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[StudentController] Exception: {ex.Message} \n Inner: {ex.InnerException?.Message}");
                return BadRequest($"Unexpected error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
