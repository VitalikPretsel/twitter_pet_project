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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public AccountController(IUserRepository repository, IMapper mapper)
        {
            userRepository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserVm> GetCurrentUser()
        {
            return Ok(mapper.Map<UserVm>(userRepository.FindUserByName(User.Identity.Name)));
        }

        [HttpGet("userName")]
        public ActionResult<string> GetCurrentUserName()
        {
            return Ok(User.Identity.Name);
        }
    }
}
