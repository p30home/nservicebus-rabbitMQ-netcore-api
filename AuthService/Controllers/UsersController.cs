using AuthService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginViewModel model)
        {
            var token = _userService.Authenticate(model.Email, model.Password);

            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new { token = token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterViewModel model)
        {
            try
            {
                var user = _userService.Register(model.FirstName, model.LastName, model.Email, model.Password);
                return Ok(new { message = $"{user.Username} created successfully, now you can login with provided username and password" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}