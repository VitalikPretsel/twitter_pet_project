using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using DAL.Repositories;
using DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            userRepository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVm>>> Get()
        {
            return Ok(mapper.Map<List<UserVm>>(await userRepository.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserVm>> Get(int id)
        {
            UserVm userVm = mapper.Map<UserVm>(await userRepository.Get(id));
            if (userVm == null)
            {
                return NotFound();
            }
            return Ok(userVm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await userRepository.Add(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!userRepository.Any(user.Id))
            {
                return NotFound();
            }
            await userRepository.Update(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            await userRepository.Delete(user);
            return Ok();
        }
    }
}
