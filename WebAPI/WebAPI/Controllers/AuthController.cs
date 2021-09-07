using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly AuthService authService;

        public AuthController(IUserRepository repository, AuthService service)
        {
            userRepository = repository;
            authService = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            if (signupModel == null || 
                userRepository.FindUserByName(signupModel.UserName) != null ||
                userRepository.FindUserByEmail(signupModel.Email) != null)
            {
                return BadRequest();
            }
            else
            {
                User user = new User()
                {
                    UserName = signupModel.UserName,
                    Email = signupModel.Email,
                    Password = signupModel.Password
                };
                await userRepository.Add(user);
                Authenticate(user);
                return Ok();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }
            User user = userRepository.FindUserByLoginModel(loginModel);
            if (user != null)
            {
                Authenticate(user);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        private void Authenticate(User user)
        {
            var token = authService.GetTokenString(user);
            Response.Cookies.Append(TokenConstants.TokenName, token,
                new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
        }

        [HttpGet]
        public ActionResult<bool> IsAuthenticated()
        {
            var tokenString = Request.Cookies[TokenConstants.TokenName];
            return Ok(!authService.IsTokenExpired(tokenString));
        }

        [HttpDelete]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(TokenConstants.TokenName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
            return Ok();
        }
    }
}
