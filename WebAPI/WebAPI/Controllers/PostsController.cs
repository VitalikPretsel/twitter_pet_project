using System;
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
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository repository)
        {
            postRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var posts = await postRepository.GetAll();
            return Ok(posts);
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<Post>>> Get(int step, int id = -1)
        {
            return Ok(await postRepository.GetFewPosts(step, id));
        }

        [HttpGet("{profileId}/details")]
        public async Task<ActionResult<IEnumerable<Post>>> Get(int step, int profileId, int id = -1)
        {
            return Ok(await postRepository.GetFewOfProfilePosts(step, profileId, id));
        }

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
