using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IApplicationContext appContext;

        public ProfilesController(IApplicationContext context)
        {
            appContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> Get()
        {
            return await appContext.Profiles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> Get(int id)
        {
            Profile profile = await appContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
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
            appContext.Profiles.Add(profile);
            await appContext.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpPut]
        public async Task<ActionResult<Profile>> Put(Profile profile)
        {
            if (profile == null)
            {
                return BadRequest();
            }
            if (!appContext.Profiles.Any(p => p.Id == profile.Id))
            {
                return NotFound();
            }
            appContext.Profiles.Update(profile);
            await appContext.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Profile>> Delete(int id)
        {
            Profile profile = await appContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return NotFound();
            }
            appContext.Profiles.Remove(profile);
            await appContext.SaveChangesAsync();
            return Ok(profile);
        }
    }
}
