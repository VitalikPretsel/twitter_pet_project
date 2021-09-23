using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.ViewModels;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

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
                user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);
                Authenticate(user);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            string accessToken = Request.Cookies[TokenConstants.AccessTokenName];
            string refreshToken = Request.Cookies[TokenConstants.RefreshTokenName];
            if (accessToken == null)
            {
                return BadRequest("No access token was provided.");
            }

            var principal = authService.GetPrincipalFromExpiredToken(accessToken);
            User user = userRepository.FindUserByName(principal.Identity.Name);
            
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Refresh token is not valid.");
            }

            SetAccessToken(user);

            return Ok();
        }

        [NonAction]
        public void Revoke()
        {
            User user = userRepository.FindUserByName(User.Identity.Name);
            if (user == null)
            {
                return;
            }

            user.RefreshToken = null;
            userRepository.Update(user);
        }

        [NonAction]
        private void Authenticate(User user)
        {
            string accessToken = authService.GetAccessTokenString(user);
            string refreshToken = authService.GetRefreshTokenString();
            user.RefreshToken = refreshToken;
            userRepository.Update(user);

            CookieOptions options = new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true };
            Response.Cookies.Append(TokenConstants.AccessTokenName, accessToken, options);
            Response.Cookies.Append(TokenConstants.RefreshTokenName, refreshToken, options);
        }

        [NonAction]
        private void SetAccessToken(User user)
        {
            string accessToken = authService.GetAccessTokenString(user);
            CookieOptions options = new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true };
            Response.Cookies.Append(TokenConstants.AccessTokenName, accessToken, options);
        }

        [HttpGet]
        public ActionResult<bool> IsAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        [HttpDelete]
        public IActionResult Logout()
        {
            CookieOptions options = new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true };
            Response.Cookies.Delete(TokenConstants.AccessTokenName, options);
            Response.Cookies.Delete(TokenConstants.RefreshTokenName, options);
            Revoke();
            return Ok();
        }
    }
}
