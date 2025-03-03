using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Scholario.Application.Dtos;
using Scholario.Application.Interfaces;

namespace Scholario.API.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            try
            {
                await _accountService.RegisterUser(registerUserDto);
                return Ok("User added successfully");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($">[GradeCtr] Received null value: {ex.Message}");
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">[GradeCtr] Unhandled exception: {ex.Message}");
                return BadRequest($"Unexpected error: {ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                string token = await _accountService.GenerateJwt(loginDto);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
