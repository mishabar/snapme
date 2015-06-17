using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Tokens;

namespace SNAPME.Services.Interfaces
{
    public interface IProductService
    {
        ProductToken GetById(string id);

        bool ToggleLike(string userId, string productId);

        bool ToggleFavorite(string userId, string productId);

        UserPreferencesToken GetPreferences(string id, string userId);

        IEnumerable<ProductToken> GetFavorites(string userId);

        IEnumerable<ProductToken> GetAll();

        IEnumerable<ProductToken> GetAllWithPreferences(string userId);

        IEnumerable<UserActionToken> GetFullSocialFeed(string productId);

        IEnumerable<UserActionToken> GetSocialFeed(string productId, string userId);
    }
}
