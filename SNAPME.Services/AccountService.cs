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

        public AccountService(IUserDetailsRepository userDetailsRepository, IUserPreferencesRepository userPreferencesRepository, IUserSnapsRepository userSnapsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
            _userPreferencesRepository = userPreferencesRepository;
            _userSnapsRepository = userSnapsRepository;
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
    }
}
