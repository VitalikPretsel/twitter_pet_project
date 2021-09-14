using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Entities;

namespace DAL.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindUserByName(string userName);
        User FindUserByEmail(string userEmail);

    }
}
