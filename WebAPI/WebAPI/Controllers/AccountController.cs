using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using DAL.Entities;
using System.Threading.Tasks;
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
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var tokenString = Request.Cookies[TokenConstants.TokenName];
            string email = authService.GetClaims(tokenString).ToList()
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            var users = await userRepository.GetAll();
            return users.FirstOrDefault(user => user.Email == email);
        }

    }
}
