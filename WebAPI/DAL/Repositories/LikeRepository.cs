using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;

namespace DAL.Repositories
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(ApplicationContext context) : base(context)
        {
        }

        public int GetPostLikesAmount(int postId)
        {
            return appContext.Likes.Where(l => l.PostId == postId).Count();
        }
    }
}
