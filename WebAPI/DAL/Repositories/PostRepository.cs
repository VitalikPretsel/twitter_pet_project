﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetFewProfilePosts(int[] profileIds, int step, int id = -1)
        {
            if (id == -1)
            {
                id = (await appContext.Posts.Where(p => profileIds.Any(id => id == p.ProfileId)).
                    OrderByDescending(p => p.Id).FirstAsync()).Id;
            }
            return appContext.Posts.Where(p => profileIds.Any(id => id == p.ProfileId)).
                OrderByDescending(p => p.Id).Where(p => p.Id <= id).Take(step).AsEnumerable();
        }

        public int GetProfilePostsAmount(int profileId)
        {
            return appContext.Posts.Where(p => p.ProfileId == profileId).Count();
        }

    }
}
