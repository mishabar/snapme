using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;
using SNAPME.Data.Repositories;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;

namespace SNAPME.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ISocialFeedRepository _socialFeedRepository;
        private IUserPreferencesRepository _userPreferencesRepository;

        public ProductService(IProductRepository productRepository, IUserPreferencesRepository userPreferencesRepository, ISocialFeedRepository socialFeedRepository)
        {
            _productRepository = productRepository;
            _socialFeedRepository = socialFeedRepository;
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
            if (likes)
            {
                _socialFeedRepository.Add(userId, productId, UserAction.LIKED);
            }

            return likes;
        }


        public bool ToggleFavorite(string userId, string productId)
        {
            bool favors = _userPreferencesRepository.Favors(userId, productId);

            if (favors)
            {
                _socialFeedRepository.Add(userId, productId, UserAction.FAVORED);
            }

            return favors;
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


        public IEnumerable<ProductToken> GetAll()
        {
            return _productRepository.GetAll().Select(p => p.AsToken());
        }


        public IEnumerable<ProductToken> GetAllWithPreferences(string userId)
        {
            var prefs = _userPreferencesRepository.GetById(userId, null);
            return _productRepository.GetAll().Select(p => p.AsToken(prefs));
        }


        public IEnumerable<UserActionToken> GetFullSocialFeed(string productId)
        {
            return _socialFeedRepository.GetByProductId(productId, null).Select(f => f.AsToken());
        }


        public IEnumerable<UserActionToken> GetSocialFeed(string productId, string userId)
        {
            return _socialFeedRepository.GetByProductId(productId, userId).Select(f => f.AsToken());
        }


        public IEnumerable<CategoryToken> GetCategories()
        {
            return CategoryToken.Generate(5);
        }
    }
}
