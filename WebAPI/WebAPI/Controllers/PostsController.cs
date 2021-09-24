using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using DAL.Repositories;
using DAL.ViewModels;
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
        private readonly IMapper mapper;

        public PostsController(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostVm>>> Get()
        {
            return Ok(mapper.Map<List<PostVm>>(await postRepository.GetAll()));
        }

        [AllowAnonymous]
        [HttpGet("getFewProfilePosts/details")]
        public async Task<ActionResult<IEnumerable<PostVm>>> Get(int step, [FromQuery] int[] profileIds, int? id = null)
        {
            return Ok(mapper.Map<List<PostVm>>(await postRepository.GetFewProfilePosts(profileIds, step, id)));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PostVm>> Get(int id)
        {
            PostVm postViewModel = mapper.Map<PostVm>(await postRepository.Get(id));
            if (postViewModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(postViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            await postRepository.Add(post);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Post post)
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
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Post post = await postRepository.Get(id);
            if (post == null)
            {
                return NotFound();
            }
            await postRepository.Delete(post);
            return Ok();
        }
    }
}
