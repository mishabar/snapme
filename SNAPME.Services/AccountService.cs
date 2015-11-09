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

        public AccountService(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
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
    }
}
