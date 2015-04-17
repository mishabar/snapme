using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens.Admin
{
    public class SellerToken
    {
        public string id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string website { get; set; }
        public string business_type { get; set; }
        public string legal_name { get; set; }
        [Required]
        public string trading_name { get; set; }
        public string tax_id { get; set; }
        public string address { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string pickup_address { get; set; }
        public string comments { get; set; }
        public bool has_account { get; set; }
        public bool is_archived { get; set; }
    }

    public static class SellerTokenExtensions
    {
        public static SellerToken AsToken(this Seller seller)
        {
            return new SellerToken 
            {
                id = seller.id,
                name = seller.name,
                email = seller.email,
                website = seller.website,
                business_type = seller.business_type,
                legal_name = seller.legal_name,
                trading_name = seller.trading_name,
                tax_id = seller.tax_id,
                address = seller.address,
                zip = seller.zip,
                city = seller.city,
                state = seller.state,
                country = seller.country,
                phone = seller.phone,
                pickup_address = seller.pickup_address,
                comments = seller.comments,
                is_archived = seller.is_archived,
                has_account = false
            };
        }

        public static Seller AsSeller(this SellerToken seller)
        {
            return new Seller
            {
                id = seller.id,
                name = seller.name,
                email = seller.email,
                website = seller.website,
                business_type = seller.business_type,
                legal_name = seller.legal_name,
                trading_name = seller.trading_name,
                tax_id = seller.tax_id,
                address = seller.address,
                zip = seller.zip,
                city = seller.city,
                state = seller.state,
                country = seller.country,
                phone = seller.phone,
                comments = seller.comments,
                pickup_address = seller.pickup_address,
                is_archived = seller.is_archived
            };
        }
    }
}
