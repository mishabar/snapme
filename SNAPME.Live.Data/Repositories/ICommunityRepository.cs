using MongoDB.Driver;
using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Repositories
{
    public interface ICommunityRepository
    {
        Task<IEnumerable<Community>> ListCommunities();

        Task<Data.Entities.Community> GetCommunity(string name);
    }
}
