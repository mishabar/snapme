using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class UserPreferencesToken
    {
        public bool likes { get; set; }
        public bool favors { get; set; }
    }

    public static class UserPreferencesTokenExtensions
    {
        public static UserPreferencesToken AsToken(this UserPreferences prefs, string productId)
        {
            return new UserPreferencesToken 
            {
                likes = prefs.likes != null && prefs.likes.Contains(productId),
                favors = prefs.favorites != null && prefs.favorites.Contains(productId)
            };
        }
    }
}
