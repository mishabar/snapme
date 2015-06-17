using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Services.Interfaces
{
    public interface IFriendsService
    {
        void StoreFriends(long me, string name, string iisnap_id, IEnumerable<SNAPME.Tokens.Facebook.UserToken> friends);
    }
}
