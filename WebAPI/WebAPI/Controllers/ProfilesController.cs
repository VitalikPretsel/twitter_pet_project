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
        public async Task<ActionResult<IEnumerable<ProfileVm>>> Get()
        {
            return Ok(mapper.Map<List<ProfileVm>>(await profileRepository.GetAll()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileVm>> Get(int id)
        {
            ProfileVm profileViewModel = mapper.Map<ProfileVm>(await profileRepository.Get(id));
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
        public ActionResult<ProfileVm> GetByName(string profileName)
        {
            ProfileVm profileViewModel = mapper.Map<ProfileVm>(profileRepository.GetByProfileName(profileName));
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
        public async Task<ActionResult<IEnumerable<ProfileVm>>> GetUserProfiles(int id)
        {
            return Ok(mapper.Map<List<ProfileVm>>(await profileRepository.GetUserProfiles(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post(DAL.Entities.Profile profile)
        {
            if (profileRepository.GetByProfileName(profile.ProfileName) != null)
            {
                ModelState.AddModelError("ProfileName", "Profile with such profilename already exists");
                return BadRequest(ModelState);
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
