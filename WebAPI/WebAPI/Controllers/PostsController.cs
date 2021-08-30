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
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository repository)
        {
            postRepository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var posts = await postRepository.GetAll();
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("getFewProfilePosts/details")]
        public async Task<ActionResult<IEnumerable<Post>>> Get(int step, int id, [FromQuery] int[] profileIds)
        {
            return Ok(await postRepository.GetFewProfilePosts(profileIds, step, id));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            Post post = await postRepository.Get(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [AllowAnonymous]
        [HttpGet("profilePostsAmount/{profileId}")]
        public ActionResult<int> GetProfilePostsAmount(int profileId)
        {
            return Ok(postRepository.GetProfilePostsAmount(profileId));
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Post(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            await postRepository.Add(post);
            return Ok(post);
        }

        [HttpPut]
        public async Task<ActionResult<Post>> Put(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            if (!postRepository.Any(post.Id))
            {
                return NotFound();
            }
            await postRepository.Update(post);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            Post post = await postRepository.Get(id);
            if (post == null)
            {
                return NotFound();
            }
            await postRepository.Delete(post);
            return Ok(post);
        }
    }
}
