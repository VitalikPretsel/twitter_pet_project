using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<Like> Get(int profileId, int postId)
        {
            return await appContext.Likes.FirstOrDefaultAsync(l => l.ProfileId == profileId && l.PostId == postId);
        }

        public async Task<bool> Any(int profileId, int postId)
        {
            return await appContext.Likes.AnyAsync(l => l.ProfileId == profileId && l.PostId == postId);
        }
    }
}
