using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ReplyRepository : GenericRepository<Reply>, IReplyRepository
    {
        public ReplyRepository(ApplicationContext context) : base(context)
        {

        }

        public int GetRepliesOnPostAmount(int postId)
        {
            return appContext.Replies.Where(r => r.PostId == postId).Count();
        }
    }
}
