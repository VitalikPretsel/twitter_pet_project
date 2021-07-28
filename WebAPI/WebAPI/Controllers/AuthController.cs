using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
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
                return Ok(new { Token = authService.GetTokenString() });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
