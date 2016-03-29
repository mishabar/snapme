using SNAPME.Live.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<SimpleProductToken>> GetCommunityProducts(long[] communityIds);

        Task<FullProductToken> GetProduct(string name);
    }
}
