using SNAPME.Live.Data.Repositories;
using SNAPME.Live.Services.Interfaces;
using SNAPME.Live.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<SimpleProductToken>> GetCommunityProducts(long[] communityIds)
        {
            return (await _productRepository.GetCommunityProducts(communityIds)).Select(p => new SimpleProductToken(p));
        }


        public async Task<FullProductToken> GetProduct(string name)
        {
            var product = await _productRepository.GetProduct(name);
            return product == null ? null : new FullProductToken(product);
        }
    }
}
