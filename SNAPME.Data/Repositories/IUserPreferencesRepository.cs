using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface IUserPreferencesRepository
    {
        bool Likes(string userId, string productId);

        bool Favors(string userId, string productId);

        UserPreferences GetById(string userId, string id);
    }
}
