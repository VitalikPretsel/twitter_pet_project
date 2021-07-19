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
    public class RepliesController : ControllerBase
    {
        private readonly ApplicationContext appContext;
        
        public RepliesController(ApplicationContext context)
        {
            appContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reply>>> Get()
        {
            return await appContext.Replies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reply>> Get(int id)
        {
            Reply reply = await appContext.Replies.FirstOrDefaultAsync(r => r.Id == id);
            if (reply == null)
            {
                return NotFound();
            }
            return new ObjectResult(reply);
        }
        
        [HttpPost]
        public async Task<ActionResult<Reply>> Post(Reply reply)
        {
            if (reply == null)
            {
                return BadRequest();
            }
            appContext.Replies.Add(reply);
            await appContext.SaveChangesAsync();
            return Ok(reply);
        }

        [HttpPut]
        public async Task<ActionResult<Reply>> Put(Reply reply)
        {
            if (reply == null)
            {
                return BadRequest();
            }
            if (!appContext.Replies.Any(r => r.Id == reply.Id))
            {
                return NotFound();
            }
            appContext.Update(reply);
            await appContext.SaveChangesAsync();
            return Ok(reply);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reply>> Delete(int id)
        {
            Reply reply = await appContext.Replies.FirstOrDefaultAsync(r => r.Id == id);
            if (reply == null)
            {
                return NotFound();
            }
            appContext.Remove(reply);
            await appContext.SaveChangesAsync();
            return Ok(reply);
        }
    }
}
