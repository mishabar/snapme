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
    public class SaleService : ISaleService
    {
        private ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public Tokens.SaleToken GetActive(string id)
        {
            var sale = _saleRepository.GetById(id, true);
            return sale != null ? sale.AsToken() : null;
        }
    }
}
