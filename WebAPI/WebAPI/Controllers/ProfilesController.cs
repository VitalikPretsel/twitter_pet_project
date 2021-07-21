﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> Get()
        {
            var profiles = await profileRepository.GetAll();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> Get(int id)
        {
            Profile profile = await profileRepository.Get(id);
            if (profile == null)
            {
                return NotFound();
            }
            return new ObjectResult(profile);
        }

        [HttpPost]
        public async Task<ActionResult<Profile>> Post(Profile profile)
        {
            if (profile == null)
            {
                return BadRequest();
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
