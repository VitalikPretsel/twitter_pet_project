using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public interface ILikeRepository : IGenericRepository<Like>
    {
        Task<Like> Get(int profileId, int postId);
        Task<bool> Any(int profileId, int postId);
    }
}
