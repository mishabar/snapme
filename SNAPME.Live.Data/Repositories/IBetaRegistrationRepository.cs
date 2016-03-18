using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Repositories
{
    public interface IBetaRegistrationRepository
    {
        Task<int> Register(string email);
    }
}
