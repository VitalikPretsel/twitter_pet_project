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
    public class LikesController : ControllerBase
    {
        private readonly ILikeRepository likeRepository;

        public LikesController(ILikeRepository repository)
        {
            likeRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> Get()
        {
            var likes = await likeRepository.GetAll();
            return Ok(likes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> Get(int id)
        {
            Like like = await likeRepository.Get(id);
            if (like == null)
            {
                return NotFound();
            }
            return Ok(like);
        }

        [HttpPost]
        public async Task<ActionResult<Like>> Post(Like like)
        {
            if (like == null)
            {
                return BadRequest();
            }
            await likeRepository.Add(like);
            return Ok(like);
        }

        [HttpPut]
        public async Task<ActionResult<Like>> Put(Like like)
        {
            if (like == null)
            {
                return BadRequest();
            }
            if (!likeRepository.Any(like.Id))
            {
                return NotFound();
            }
            await likeRepository.Update(like);
            return Ok(like);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Like>> Delete(int id)
        {
            Like like = await likeRepository.Get(id);
            if (like == null)
            {
                return NotFound();
            }
            await likeRepository.Delete(like);
            return Ok(like);
        }
    }
}
