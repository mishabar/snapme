using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SNAPME.Tokens;

namespace SNAPME.Web.Models.Account
{
    public enum AccountMenuSection
    {
        Details,
        Points,
        Drops,
        Snaps,
        Favorites,
        Address
    }

    public class MyAccountBaseModel
    {
        public AccountMenuSection ActiveSection { get; set; }
        public int Points { get; set; }
        public int DropsCount { get; set; }
        public int SnapsCount { get; set; }
        public int FavoritesCount { get; set; }
        public double Savings { get; set; }
    }

    public class MyAccountDetailsModel : MyAccountBaseModel
    {
    }

    public class MyAccountPointsModel : MyAccountBaseModel
    {
    }

    public class MyAccountSnapsModel : MyAccountBaseModel
    {
        public IEnumerable<SaleToken> Snaps { get; set; }
    }

    public class MyAccountFavoritesModel : MyAccountBaseModel
    {
        
    }

    public class MyAccountDropsModel : MyAccountBaseModel
    {
        public IEnumerable<SaleToken> Drops { get; set; }
    }

    public class MyAccountAddressModel : MyAccountBaseModel
    {
        
    }
}