using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface IFriendRepository
    {
        void StoreFriends(long me, string name, string iisnap_id, Dictionary<long, string> friends);
    }
}
