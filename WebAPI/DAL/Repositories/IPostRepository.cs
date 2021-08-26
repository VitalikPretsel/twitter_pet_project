using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IEnumerable<Post>> GetFewPosts(int step, int id = -1);
        Task<IEnumerable<Post>> GetFewOfProfilePosts(int step, int profileId, int id = -1);
        int GetProfilePostsAmount(int profileId);
    }
}
