using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<LoginDto> _signInManager;
        private readonly UserManager<Users> _userManager;

        public LoginController(SignInManager<LoginDto> signInManager, UserManager<Users> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                return BadRequest("Invalid login attempt.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // You can customize the response as needed
                return Ok("Login successful!");
            }

            if (result.IsLockedOut)
            {
                return BadRequest("User account locked out.");
            }
            else
            {
                return BadRequest("Invalid login attempt.");
            }
        }
    }
}
