using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data.Repositories;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;

namespace SNAPME.Services
{
    public class AccountService : IAccountService
    {
        private IUserDetailsRepository _userDetailsRepository;
        private IUserPreferencesRepository _userPreferencesRepository;
        private IUserSnapsRepository _userSnapsRepository;
        private IFriendRepository _friendsRepository;

        public AccountService(IUserDetailsRepository userDetailsRepository, IUserPreferencesRepository userPreferencesRepository,
            IUserSnapsRepository userSnapsRepository, IFriendRepository friendsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
            _userPreferencesRepository = userPreferencesRepository;
            _userSnapsRepository = userSnapsRepository;
            _friendsRepository = friendsRepository;
        }

        public UserDetailsToken Get(string id)
        {
            var details = _userDetailsRepository.GetDetails(id).AsToken();

            if (string.IsNullOrWhiteSpace(details.id))
            {
                details.id = id;
            }

            return details;
        }


        public IEnumerable<AddressToken> GetAddresses(string id)
        {
            var userDetails = Get(id);

            return userDetails.addresses;
        }

        
        public AddressToken AddAddress(string id, AddressToken addressToken)
        {
            return _userDetailsRepository.AddAddress(id, addressToken.AsAddress()).AsToken();
        }

        public AddressToken UpdateAddress(string id, int idx, AddressToken addressToken)
        {
            return _userDetailsRepository.UpdateAddress(id, idx, addressToken.AsAddress()).AsToken();
        }


        public UserDetailsToken Save(UserDetailsToken token)
        {
            return _userDetailsRepository.Save(token.AsUserDetails()).AsToken();
        }


        public bool DeleteAddress(string id, int idx)
        {
            return _userDetailsRepository.DeleteAddress(id, idx);
        }


        public IEnumerable<ProductInfoToken> GetFavorites(string id)
        {
            return _userPreferencesRepository.AllFavorites(id).Select(c => new ProductInfoToken{ id = c.Key, name = c.Value });
        }


        public IEnumerable<UserSnapToken> GetSnaps(string id)
        {
            return _userSnapsRepository.GetSnaps(id).Select(s => s.AsToken());
        }


        public void EnsureRecord(UserDetailsToken userDetailsToken)
        {
            _userDetailsRepository.EnsureRecord(userDetailsToken.AsUserDetails());
        }


        public IEnumerable<RewardToken> GetRewards(string id)
        {
            return new RewardToken[] {
                new RewardToken { type = RewardType.Purchase, points = 113 },
                new RewardToken { type = RewardType.DirectInvitation, points = 75 },
                new RewardToken { type = RewardType.PurchaseCommission, points = 54 },
                new RewardToken { type = RewardType.SpecialOffers, points = 0 }
            };
        }


        public IEnumerable<FriendToken> GetFriends(string id)
        {
            return _friendsRepository.GetFriends(id).Select(f => new FriendToken { fb_id = f.Key, name = f.Value, image_url = string.Format("http://graph.facebook.com/{0}/picture?type=square&width=200&height=200", f.Key) });
        }
    }
}
