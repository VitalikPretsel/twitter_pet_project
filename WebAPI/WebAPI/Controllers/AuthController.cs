using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }
            User user = userRepository.FindUserByLoginModel(loginModel);
            if (user != null)
            {
                var token = authService.GetTokenString();
                Response.Cookies.Append(TokenConstants.TokenName, token,
                    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
