using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;
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

        public ProfilesController(IProfileRepository repository)
        {
            profileRepository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> Get()
        {
            var profiles = await profileRepository.GetAll();
            return Ok(profiles);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> Get(int id)
        {
            Profile profile = await profileRepository.Get(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [AllowAnonymous]
        [HttpGet("getByName/{profileName}")]
        public ActionResult<Profile> GetByName(string profileName)
        {
            Profile profile = profileRepository.GetByProfileName(profileName);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpGet("getUserProfiles/{userId}")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetUserProfiles(int userId)
        {
            return Ok(await profileRepository.GetUserProfiles(userId));
        }

        [AllowAnonymous]
        [HttpGet("profileName/{id}")]
        public async Task<ActionResult<string>> GetProfileName(int id)
        {
            Profile profile = await profileRepository.Get(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile.ProfileName);
        }

        [HttpPost]
        public async Task<ActionResult<Profile>> Post(Profile profile)
        {
            if (profileRepository.GetByProfileName(profile.ProfileName) != null)
            {
                ModelState.AddModelError("ProfileName", "Profile with such profilename already exists");
                return BadRequest(ModelState);
            }
            await profileRepository.Add(profile);
            return Ok(profile);
        }

        [HttpPut]
        public async Task<ActionResult<Profile>> Put(Profile profile)
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
            return Ok(profile);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Profile>> Delete(int id)
        {
            Profile profile = await profileRepository.Get(id);
            if (profile == null)
            {
                return NotFound();
            }
            await profileRepository.Delete(profile);
            return Ok(profile);
        }
    }
}
