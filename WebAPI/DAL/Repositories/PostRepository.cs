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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetFewPosts(int step, int id = -1)
        {
            var posts = appContext.Posts.AsEnumerable().Reverse();
            if (id == -1)
            {
                id = posts.First().Id;
            }
            return posts.Where(p => p.Id <= id).Take(step);
        }
    }
}
