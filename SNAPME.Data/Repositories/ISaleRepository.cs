using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface ISaleRepository
    {
        Sale GetById(string id, bool active);

        IEnumerable<Sale> GetActive();

        void Save(Sale sale);

        void Create(Sale sale);

        Sale GetScheduledSale(string productId);

        void AdjustSalesStatus();

        Sale JoinSale(string userId, string productId);
    }
}
