using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Interfaces;
using System.Security.Claims;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService) 
        {
            _groupService = groupService;
        }
        [HttpGet]
        [Authorize(Roles ="Teacher, Parent, Student")]
        public async Task<IActionResult> GetLoggedUserGroup() //Grupy dla poszczególnych kont(grupa nauczyciela to grupa, której jest wychowawcą)
        {
            try
            {
                var userIdClaim = (User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userIdClaim == null)
                {
                    return Unauthorized("User's ID is missing in the token.");
                }
                var userId = int.Parse(userIdClaim);
                var subjects = await _groupService.GetLoggedUserGroup(userId);

                return Ok(subjects);
            }
            catch (ArgumentOutOfRangeException ex)
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
