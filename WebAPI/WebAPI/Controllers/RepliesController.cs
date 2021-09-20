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
    public class RepliesController : ControllerBase
    {
        private readonly IReplyRepository replyRepository;

        public RepliesController(IReplyRepository repository)
        {
            replyRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reply>>> Get()
        {
            var replies = await replyRepository.GetAll();
            return Ok(replies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reply>> Get(int id)
        {
            Reply reply = await replyRepository.Get(id);
            if (reply == null)
            {
                return NotFound();
            }
            return Ok(reply);
        }

        [AllowAnonymous]
        [HttpGet("OnPostAmount/{postId}")]
        public ActionResult<int> GetRepliesOnPost(int postId)
        {
            return Ok(replyRepository.GetPostRepliesAmount(postId));
        }

        [HttpPost]
        public async Task<ActionResult<Reply>> Post(Reply reply)
        {
            if (reply == null)
            {
                return BadRequest();
            }
            await replyRepository.Add(reply);
            return Ok(reply);
        }

        [HttpPut]
        public async Task<ActionResult<Reply>> Put(Reply reply)
        {
            if (reply == null)
            {
                return BadRequest();
            }
            if (!replyRepository.Any(reply.Id))
            {
                return NotFound();
            }
            await replyRepository.Update(reply);
            return Ok(reply);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reply>> Delete(int id)
        {
            Reply reply = await replyRepository.Get(id);
            if (reply == null)
            {
                return NotFound();
            }
            await replyRepository.Delete(reply);
            return Ok(reply);
        }
    }
}
