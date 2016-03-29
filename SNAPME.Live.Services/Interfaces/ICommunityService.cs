using SNAPME.Live.Data.Entities;
using SNAPME.Live.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Interfaces
{
    public interface ICommunityService
    {
        Task<IEnumerable<CommunityToken>> ListCommunities();
        Task<CommunityToken> GetCommunity(string name);
    }
}
