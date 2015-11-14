using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Tokens;

namespace SNAPME.Services.Interfaces
{
    public interface IAccountService
    {
        UserDetailsToken Get(string id);

        AddressToken AddAddress(string id, AddressToken addressToken);

        AddressToken UpdateAddress(string id, int idx, AddressToken addressToken);

        UserDetailsToken Save(UserDetailsToken token);

        bool DeleteAddress(string id, int idx);

        IEnumerable<ProductInfoToken> GetFavorites(string id);

        IEnumerable<UserSnapToken> GetSnaps(string id);

        void EnsureRecord(UserDetailsToken userDetailsToken);

        IEnumerable<RewardToken> GetRewards(string id);

        IEnumerable<FriendToken> GetFriends(string id);
    }
}
