using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface IUserDetailsRepository
    {
        UserDetails GetDetails(string id);

        Address AddAddress(string id, Address address);

        Address UpdateAddress(string id, int idx, Address address);

        UserDetails Save(UserDetails userDetails);

        bool DeleteAddress(string id, int idx);
    }
}
