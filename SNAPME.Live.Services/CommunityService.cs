using SNAPME.Live.Data.Entities;
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
    public class CommunityService : ICommunityService
    {
        private ICommunityRepository _communityRepository;
        private IProductRepository _productRepository;

        public CommunityService(ICommunityRepository communityRepository, IProductRepository productRepository)
        {
            _communityRepository = communityRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<CommunityToken>> ListMyCommunities()
        {
            var communities = await _communityRepository.ListCommunities();
            var products = await _productRepository.GetCommunityProducts(communities.Select(c => c.id).ToArray());

            return communities.Select(c => new CommunityToken(c, products.Where(p => p.community_id == c.id)));
        }

        public async Task<IEnumerable<CommunityToken>> ListCommunities()
        {
            var communities = await _communityRepository.ListCommunities();
            //var products = await _productRepository.GetCommunityProducts(communities.Select(c => c.id).ToArray());

            return communities.Select(c => new CommunityToken(c, Enumerable.Empty<Product>()));
        }

        public async Task<CommunityToken> GetCommunity(string name)
        {
            var community = await _communityRepository.GetCommunity(name);
            var products = await _productRepository.GetCommunityProducts(new long[] { community.id });

            return new CommunityToken(community, products);
        }
    }
}
