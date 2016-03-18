using SNAPME.Live.Data.Repositories;
using SNAPME.Live.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services
{
    public class BetaRegistrationService : IBetaRegistrationService
    {
        private IBetaRegistrationRepository _repository;

        public BetaRegistrationService(IBetaRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Register(string email)
        {
            return await _repository.Register(email);
        }
    }
}
