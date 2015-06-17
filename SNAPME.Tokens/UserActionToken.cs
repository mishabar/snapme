using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class UserActionToken
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string action { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public DateTime ts { get; set; }
    }

    public static class UserActionTokenExtensions
    {
        private static readonly Dictionary<int, string> _actions = new Dictionary<int, string> { 
            {UserAction.LIKED, "{0} liked the product."},
            {UserAction.FAVORED, "{0} added the product to favorites."},
            {UserAction.JOINED_SALE, "{0} joined the sale."},
            {UserAction.BOUGHT, "{0} bought the product."}
        };

        public static UserActionToken AsToken(this UserAction action)
        {
            return new UserActionToken 
            {
                id = action.id,
                user_id = action.user_id,
                user_name = action.user_name,
                product_id = action.product_id,
                product_name = action.product_name,
                ts = action.ts,
                action = string.Format(_actions[action.action], action.user_name)
            };
        }
    }
}
