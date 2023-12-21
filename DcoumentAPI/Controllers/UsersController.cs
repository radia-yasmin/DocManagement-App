using DcoumentAPI.Domain.Dtos;
using DcoumentAPI.Domain.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DcoumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<GetAllUsersDto> userList = new List<GetAllUsersDto>();
            var users =await _userManager.Users.ToListAsync();

            if (users.Count() > 0)
            {
                foreach (var item in users)
                {
                    GetAllUsersDto model = new GetAllUsersDto();
                    var roles = await _userManager.GetRolesAsync(item);
                    model.Email = item.Email;
                    model.Name = item.FirstName + " " + item.LastName;
                    model.UserRole = roles.FirstOrDefault();
                    userList.Add(model);
                }
            }
            return Ok(userList);
        }
    }
}
