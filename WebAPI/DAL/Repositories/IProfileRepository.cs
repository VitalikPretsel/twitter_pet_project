using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public interface IProfileRepository : IGenericRepository<Profile>
    {
        Profile GetByProfileName(string profileName);
        string GetProfileName(int id);
        Task<IEnumerable<Profile>> GetUserProfiles(int userId);
    }
}
