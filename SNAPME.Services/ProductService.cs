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
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUserPreferencesRepository _userPreferencesRepository;

        public ProductService(IProductRepository productRepository, IUserPreferencesRepository userPreferencesRepository)
        {
            _productRepository = productRepository;
            _userPreferencesRepository = userPreferencesRepository;
        }


        public Tokens.ProductToken GetById(string id)
        {
            return _productRepository.GetById(id).AsToken();
        }


        public bool ToggleLike(string userId, string productId)
        {
            bool likes = _userPreferencesRepository.Likes(userId, productId);
            _productRepository.AddLike(productId, userId, likes);

            return likes;
        }


        public bool ToggleFavorite(string userId, string productId)
        {
            return _userPreferencesRepository.Favors(userId, productId);
        }


        public UserPreferencesToken GetPreferences(string id, string userId)
        {
            return _userPreferencesRepository.GetById(userId, id).AsToken(id);
        }


        public IEnumerable<ProductToken> GetFavorites(string userId)
        {
            var prefs = _userPreferencesRepository.GetById(userId, null);
            return _productRepository.GetByIds(prefs.favorites).Select(p => p.AsToken(prefs));
        }
    }
}
