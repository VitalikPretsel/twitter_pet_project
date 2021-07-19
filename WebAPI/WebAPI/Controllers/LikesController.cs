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
    public class LikesController : ControllerBase
    {
        private readonly ApplicationContext appContext;
        
        public LikesController(ApplicationContext context)
        {
            appContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> Get()
        {
            return await appContext.Likes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> Get(int id)
        {
            Like like = await appContext.Likes.FirstOrDefaultAsync(l => l.Id == id);
            if (like == null)
            {
                return NotFound();
            }
            return new ObjectResult(like);
        }
        
        [HttpPost]
        public async Task<ActionResult<Like>> Post(Like like)
        {
            if (like == null)
            {
                return BadRequest();
            }
            appContext.Likes.Add(like);
            await appContext.SaveChangesAsync();
            return Ok(like);
        }

        [HttpPut]
        public async Task<ActionResult<Like>> Put(Like like)
        {
            if (like == null)
            {
                return BadRequest();
            }
            if (!appContext.Likes.Any(l => l.Id == like.Id))
            {
                return NotFound();
            }
            appContext.Update(like);
            await appContext.SaveChangesAsync();
            return Ok(like);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Like>> Delete(int id)
        {
            Like like = await appContext.Likes.FirstOrDefaultAsync(l => l.Id == id);
            if (like == null)
            {
                return NotFound();
            }
            appContext.Remove(like);
            await appContext.SaveChangesAsync();
            return Ok(like);
        }
    }
}
