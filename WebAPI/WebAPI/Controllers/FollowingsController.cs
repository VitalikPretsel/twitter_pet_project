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
    public class FollowingsController : ControllerBase
    {
        private readonly IFollowingRepository followingRepository;

        public FollowingsController(IFollowingRepository repository)
        {
            followingRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Following>>> Get()
        {
            var followings = await followingRepository.GetAll();
            return Ok(followings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Following>> Get(int id)
        {
            Following following = await followingRepository.Get(id);
            if (following == null)
            {
                return NotFound();
            }
            return Ok(following);
        }

        [HttpGet("profileFollowersAmount/{profileId}")]
        public ActionResult<int> GetProfileFollowersAmount(int profileId)
        {
            return Ok(followingRepository.GetProfileFollowersAmount(profileId));
        }

        [HttpGet("profileFollowingsAmount/{profileId}")]
        public ActionResult<int> GetProfileFollowingsAmount(int profileId)
        {
            return Ok(followingRepository.GetProfileFollowingsAmount(profileId));
        }

        [HttpGet("profileFollowings/{profileId}")]
        public async Task<ActionResult<IEnumerable<Following>>> GetProfileFollowings(int profileId)
        {
            return Ok(await followingRepository.GetProfileFollowings(profileId));
        }

        [HttpPost]
        public async Task<ActionResult<Following>> Post(Following following)
        {
            if (following == null)
            {
                return BadRequest();
            }
            await followingRepository.Add(following);
            return Ok(following);
        }

        [HttpPut]
        public async Task<ActionResult<Following>> Put(Following following)
        {
            if (following == null)
            {
                return BadRequest();
            }
            if (!followingRepository.Any(following.Id))
            {
                return NotFound();
            }
            await followingRepository.Update(following);
            return Ok(following);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Following>> Delete(int id)
        {
            Following following = await followingRepository.Get(id);
            if (following == null)
            {
                return NotFound();
            }
            await followingRepository.Delete(following);
            return Ok(following);
        }
    }
}
