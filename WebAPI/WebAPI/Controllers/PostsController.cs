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
    public class PostsController : ControllerBase
    {
        private readonly ApplicationContext appContext;
        
        public PostsController(ApplicationContext context)
        {
            appContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            return await appContext.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            Post post = await appContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return new ObjectResult(post);
        }
        
        [HttpPost]
        public async Task<ActionResult<Post>> Post(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            appContext.Posts.Add(post);
            await appContext.SaveChangesAsync();
            return Ok(post);
        }

        [HttpPut]
        public async Task<ActionResult<Post>> Put(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            if (!appContext.Posts.Any(p => p.Id == post.Id))
            {
                return NotFound();
            }
            appContext.Update(post);
            await appContext.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            Post post = await appContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            appContext.Remove(post);
            await appContext.SaveChangesAsync();
            return Ok(post);
        }
    }
}
