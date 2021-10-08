using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Post>> GetAll()
        {
            return await appContext.Posts.Include(p => p.Profile).Include(p => p.Likes)
                .Include(p => p.Replies).ToListAsync();
        }

        public new async Task<Post> Get(int id)
        {
            return await appContext.Posts.Include(p => p.Profile).Include(p => p.Likes)
                .Include(p => p.Replies).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetFewProfilePosts(int[] profileIds, int step, int? id)
        {
            if (id == null)
            {
                id ??= (await appContext.Posts.Where(p => profileIds.Any(id => id == p.ProfileId)).
                        OrderByDescending(p => p.Id).FirstOrDefaultAsync())?.Id;
                if (id == null)
                {
                    return null;
                }
            }
            return appContext.Posts.Include(p => p.Profile).Include(p => p.Likes).Include(p => p.Replies)
                .Where(p => profileIds.Any(id => id == p.ProfileId)).
                OrderByDescending(p => p.Id).Where(p => p.Id <= id).Take(step);
        }

        public async Task<IEnumerable<int>> GetProfileLikedPosts(int profileFollowerId, int[] profileFollowingIds, int step, int? id)
        {
            return (await GetFewProfilePosts(profileFollowingIds, step, id))
                .Where(p => p.Likes.Any(p => p.ProfileId == profileFollowerId))
                .Select(p => p.Id);
        }
    }
}
