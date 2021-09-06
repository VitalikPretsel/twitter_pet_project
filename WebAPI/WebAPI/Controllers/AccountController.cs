using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using DAL.Entities;
using System.Linq;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly AuthService authService;

        public AccountController(IUserRepository repository, AuthService service)
        {
            userRepository = repository;
            authService = service;
        }

        [HttpGet]
        public ActionResult<User> GetCurrentUser()
        {
            var tokenString = Request.Cookies[TokenConstants.TokenName];
            string email = authService.GetClaims(tokenString).ToList()
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            return Ok(userRepository.FindUserByNameOrEmail(email));
        }

        [HttpGet("userName")]
        public ActionResult<string> GetCurrentUserName()
        {
            var tokenString = Request.Cookies[TokenConstants.TokenName];
            return Ok(authService.GetClaims(tokenString).ToList()
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value);
        }
    }
}
