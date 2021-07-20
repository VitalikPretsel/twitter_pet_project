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
    public class FollowingsController : ControllerBase
    {
        private readonly ApplicationContext appContext;
        
        public FollowingsController(ApplicationContext context)
        {
            appContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Following>>> Get()
        {
            return await appContext.Followings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Following>> Get(int id)
        {
            Following following = await appContext.Followings.FirstOrDefaultAsync(f => f.Id == id);
            if (following == null)
            {
                return NotFound();
            }
            return new ObjectResult(following);
        }
        
        [HttpPost]
        public async Task<ActionResult<Following>> Following(Following following)
        {
            if (following == null)
            {
                return BadRequest();
            }
            appContext.Followings.Add(following);
            await appContext.SaveChangesAsync();
            return Ok(following);
        }

        [HttpPut]
        public async Task<ActionResult<Following>> Put(Following following)
        {
            if (following == null)
            {
                return BadRequest();
            }
            if (!appContext.Followings.Any(f => f.Id == following.Id))
            {
                return NotFound();
            }
            appContext.Update(following);
            await appContext.SaveChangesAsync();
            return Ok(following);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Following>> Delete(int id)
        {
            Following following = await appContext.Followings.FirstOrDefaultAsync(f => f.Id == id);
            if (following == null)
            {
                return NotFound();
            }
            appContext.Remove(following);
            await appContext.SaveChangesAsync();
            return Ok(following);
        }
    }
}
