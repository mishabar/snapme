using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Tokens;

namespace SNAPME.Services.Interfaces
{
    public interface ISaleService
    {
        SaleToken GetActive(string id, string userId);

        IEnumerable<SaleToken> GetAllActive(string userId);

        void SaveSale(SaleToken token);

        SaleToken GetScheduledSale(string productId, string userId);

        void AdjustSalesStatus();

        SaleToken JoinSale(string userId, string userName, string productId);
    }
}
