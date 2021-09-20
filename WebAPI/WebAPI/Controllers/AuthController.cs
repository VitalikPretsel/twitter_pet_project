using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.ViewModels;
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
        private readonly PasswordEncryptionService passwordEncryptionService;

        public AuthController(IUserRepository repository, AuthService authService, PasswordEncryptionService passwordEncryptionService)
        {
            userRepository = repository;
            this.authService = authService;
            this.passwordEncryptionService = passwordEncryptionService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupViewModel signupModel)
        {
            if (userRepository.FindUserByName(signupModel.UserName) != null)
            {
                ModelState.AddModelError("UserName", "User with such username already exists");
            }
            if (userRepository.FindUserByEmail(signupModel.Email) != null)
            {
                ModelState.AddModelError("Email", "User with such email already exists");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                string passwordHash;
                byte[] passwordSalt;
                passwordEncryptionService.EncryptPassword(signupModel.Password, out passwordHash, out passwordSalt);
                User user = new User()
                {
                    UserName = signupModel.UserName,
                    Email = signupModel.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
                await userRepository.Add(user);
                Authenticate(user);
                return Ok();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }
            User user = userRepository.FindUserByName(loginModel.UserName);
            if (user != null && 
                passwordEncryptionService.VerifyPassword(loginModel.Password, user?.PasswordHash, user?.PasswordSalt))
            {
                Authenticate(user);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [NonAction]
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
