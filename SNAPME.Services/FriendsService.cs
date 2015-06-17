using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data.Repositories;
using SNAPME.Services.Interfaces;

namespace SNAPME.Services
{
    public class FriendsService : IFriendsService
    {
        private IFriendRepository _friendRepository;

        public FriendsService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public void StoreFriends(long me, string name, string iisnap_id, IEnumerable<Tokens.Facebook.UserToken> friends)
        {
            _friendRepository.StoreFriends(me, name, iisnap_id, friends.ToDictionary(k => k.id, v => v.name));
        }
    }
}
