using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos.Message;
using Scholario.Application.Dtos.Subject;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost("subjects")]
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateSubject(CreateSubjectDto createSubjectDto)
        {
            try
            {
                await _subjectService.CreateSubject(createSubjectDto);
                return Ok("Subject create successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.Message}");
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
