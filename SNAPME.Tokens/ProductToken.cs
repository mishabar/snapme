using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class ProductBaseToken
    {
        [Required]
        public string id { get; set; }
    }
    
    public class ProductToken
    {
        public string id { get; set; }
        [Required]
        public string sku { get; set; }
        [Required]
        public string seller_id { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string short_descritpion { get; set; }
        public string descritpion { get; set; }
        [Required]
        [Range(0.01F, Double.MaxValue)]
        public double retail_price { get; set; }
        [Required]
        [Range(0.01F, Double.MaxValue)]
        public double purchase_price { get; set; }
        [Required]
        [Range(0.0F, Double.MaxValue)]
        public double suggested_sell_price { get; set; }
        public string[] images { get; set; }
        [Required]
        [Range(0.01F, Double.MaxValue)]
        public double length { get; set; }
        [Required]
        [Range(0.01F, Double.MaxValue)]
        public double width { get; set; }
        [Required]
        [Range(0.01F, Double.MaxValue)]
        public double height { get; set; }
        public string size { get; set; }
        [Required]
        public string weight { get; set; }
        [Range(0, 999)]
        public int lead_time { get; set; }
        public bool is_draft { get; set; }
        [Required]
        public bool is_dropship { get; set; }
        [Required]
        public bool is_oem { get; set; }

        // User Preferences
        public UserPreferencesToken UserPreferences { get; set; }

        // Sale
        public SaleToken Sale { get; set; }

        // Stock Model
        public string stock_model { get; set; }
        public int min_quantity { get; set; }

        public ProductToken()
        {
            is_draft = true;
        }

        public static ProductToken Generate()
        {
            return new ProductToken { 
                id = Guid.NewGuid().ToString(),
                name = Guid.NewGuid().ToString().Replace('-', ' '), 
                retail_price = new Random().Next(5000) + 10000,
                //size = "12cm x 14cm x 25cm",
                weight = "0.8kg"
            };
        }
    }

    public static class ProductTokenExtensions
    {
        public static ProductToken AsToken(this Product product)
        {
            string[] sizes = product.size.Split('x');

            return new ProductToken {
                id = product.id,
                sku = product.sku,
                seller_id = product.seller_id,
                category = product.category,
                name = product.name,
                short_descritpion = product.short_descritpion,
                descritpion = product.descritpion,
                retail_price = product.retail_price / 100F,
                purchase_price = product.purchase_price / 100F,
                suggested_sell_price = product.suggested_sell_price / 100F,
                images = product.images,
                length = double.Parse(sizes[0]),
                width = double.Parse(sizes[1]),
                height = double.Parse(sizes[2]),
                size = product.size,
                weight = product.weight,
                lead_time = product.lead_time,
                is_draft = product.is_draft,
                is_dropship = product.is_dropship,
                is_oem = product.is_oem,
                stock_model = product.stock_model,
                min_quantity = product.min_quantity
            };
        }

        public static ProductToken AsToken(this Product product, UserPreferences prefs)
        {
            var token = product.AsToken();
            token.UserPreferences = prefs.AsToken(token.id);

            return token;
        }

        public static Product AsProduct(this ProductToken product)
        {
            return new Product
            {
                id = product.id,
                sku = product.sku,
                seller_id = product.seller_id,
                name = product.name,
                category = product.category,
                short_descritpion = product.short_descritpion,
                descritpion = product.descritpion,
                retail_price = (int)Math.Floor(product.retail_price * 100F),
                purchase_price = (int)Math.Floor(product.purchase_price * 100F),
                suggested_sell_price = (int)Math.Floor(product.suggested_sell_price * 100F),
                size = string.Join("x", product.length, product.width, product.height),
                weight = product.weight,
                lead_time = product.lead_time,
                is_draft = product.is_draft,
                is_dropship = product.is_dropship,
                is_oem = product.is_oem,
                stock_model = product.stock_model,
                min_quantity = product.min_quantity
            };
        }
    }
}
