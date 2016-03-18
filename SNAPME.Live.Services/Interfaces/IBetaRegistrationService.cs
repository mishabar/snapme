using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Interfaces
{
    public interface IBetaRegistrationService
    {
        Task<int> Register(string email);
    }
}
