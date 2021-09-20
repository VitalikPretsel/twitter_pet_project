using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using DAL.Entities;
using DAL.ViewModels;
using System.Linq;
using System.Security.Claims;
using AutoMapper;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly AuthService authService;
        private readonly IMapper mapper;

        public AccountController(IUserRepository repository, IMapper mapper, AuthService service)
        {
            userRepository = repository;
            authService = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserViewModel> GetCurrentUser()
        {
            var tokenString = Request.Cookies[TokenConstants.TokenName];
            string email = authService.GetClaims(tokenString).ToList()
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            return Ok(mapper.Map<UserViewModel>(userRepository.FindUserByEmail(email)));
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
