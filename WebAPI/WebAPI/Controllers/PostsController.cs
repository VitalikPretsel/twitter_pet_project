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
        private readonly IProfileRepository profileRepository;
        private readonly ILikeRepository likeRepository;
        private readonly IReplyRepository replyRepository;
        private readonly IMapper mapper;

        public PostsController(IPostRepository postRepository, IProfileRepository profileRepository,
            ILikeRepository likeRepository, IReplyRepository replyRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.profileRepository = profileRepository;
            this.likeRepository = likeRepository;
            this.replyRepository = replyRepository;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> Get()
        {
            var postViewModels = mapper.Map<List<PostViewModel>>(await postRepository.GetAll());
            return Ok(postViewModels);
        }

        [AllowAnonymous]
        [HttpGet("getFewProfilePosts/details")]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> Get(int step, [FromQuery] int[] profileIds, int? id = null)
        {
            var postViewModels = mapper.Map<List<PostViewModel>>(await postRepository.GetFewProfilePosts(profileIds, step, id));
            return Ok(postViewModels);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewModel>> Get(int id)
        {
            PostViewModel postViewModel = mapper.Map<PostViewModel>(await postRepository.Get(id));
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

        [NonAction]
        public void GetPostViewModelInfo(PostViewModel postViewModel)
        {
            postViewModel.ProfileName = profileRepository.GetProfileName(postViewModel.ProfileId);
            postViewModel.LikesAmount = likeRepository.GetPostLikesAmount(postViewModel.Id);
            postViewModel.RepliesAmount = replyRepository.GetPostRepliesAmount(postViewModel.Id);
        }
    }
}
