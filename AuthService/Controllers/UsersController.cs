using System.Threading.Tasks;
using AuthService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NServiceBus.Logging;
using WebApi.Services;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static ILog log = LogManager.GetLogger<UsersController>();
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginViewModel model)
        {
            try
            {
                log.Info($"authentication request : {model}");
                var token = await _userService.Authenticate(model.Email, model.Password);

                if (token == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(new { token = token });
            }
            catch (Exceptions.LoginException ex)
            {
                log.Debug(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            try
            {
                log.Info($"register request : {model}");
                var user = await _userService.Register(model.FirstName, model.LastName, model.Email, model.Password);
                return Ok(new { message = $"{user.UserInfo.Username} created successfully, now you can login with provided username and password" });
            }
            catch (Exceptions.RegisterException ex)
            {
                log.Debug(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

    }
}