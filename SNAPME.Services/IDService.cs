using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data.Repositories;
using SNAPME.Services.Interfaces;

namespace SNAPME.Services
{
    public class IDService : IIDService
    {
        private IIDRepository _idRepository;

        public IDService(IIDRepository idRepository)
        {
            _idRepository = idRepository;
        }

        public string GenerateId()
        {
            return _idRepository.GenerateId();
        }
    }
}
