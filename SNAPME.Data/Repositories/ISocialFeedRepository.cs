using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface ISocialFeedRepository
    {
        void Add(string userId, string productId, int action);

        IEnumerable<UserAction> GetByProductId(string productId, string userId);
    }
}
