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
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileRepository profileRepository;
        private readonly IMapper mapper;

        public ProfilesController(IProfileRepository profileRepository, IMapper mapper)
        {
            this.profileRepository = profileRepository;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileViewModel>>> Get()
        {
            return Ok(mapper.Map<List<ProfileViewModel>>(await profileRepository.GetAll()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileViewModel>> Get(int id)
        {
            ProfileViewModel profileViewModel = mapper.Map<ProfileViewModel>(await profileRepository.Get(id));
            if (profileViewModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(profileViewModel);
            }
        }

        [AllowAnonymous]
        [HttpGet("getByName/{profileName}")]
        public ActionResult<ProfileViewModel> GetByName(string profileName)
        {
            ProfileViewModel profileViewModel = mapper.Map<ProfileViewModel>(profileRepository.GetByProfileName(profileName));
            if (profileViewModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(profileViewModel);
            }
        }

        [HttpGet("getUserProfiles/{id}")]
        public async Task<ActionResult<IEnumerable<ProfileViewModel>>> GetUserProfiles(int id)
        {
            return Ok(mapper.Map<List<ProfileViewModel>>(await profileRepository.GetUserProfiles(id)));
        }

        [AllowAnonymous]
        [HttpGet("profileName/{id}")]
        public ActionResult<string> GetProfileName(int id)
        {
            if (!profileRepository.Any(id))
            {
                return NotFound();
            }
            return Ok(profileRepository.GetProfileName(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(DAL.Entities.Profile profile)
        {
            if (profile == null)
            {
                return BadRequest();
            }
            await profileRepository.Add(profile);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(DAL.Entities.Profile profile)
        {
            if (profile == null)
            {
                return BadRequest();
            }
            if (!profileRepository.Any(profile.Id))
            {
                return NotFound();
            }
            await profileRepository.Update(profile);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DAL.Entities.Profile profile = await profileRepository.Get(id);
            if (profile == null)
            {
                return NotFound();
            }
            await profileRepository.Delete(profile);
            return Ok();
        }
    }
}
