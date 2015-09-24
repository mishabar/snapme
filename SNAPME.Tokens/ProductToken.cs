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
        public string[] images { get; set; }
        public int[] image_indexes { get; set; }
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
        public bool is_draft { get; set; }
        
        public IEnumerable<CommentToken> comments { get; set; }
        // User Preferences
        public UserPreferencesToken UserPreferences { get; set; }

        // Social Feed
        public UserActionToken[] SocialFeed { get; set; }

        // Sale
        public string sale_id { get; set; }
        public SaleToken sale { get; set; }

        // Stock Model
        public string stock_model { get; set; }
        public int min_quantity { get; set; }

        public ProductToken()
        {
            is_draft = true;
        }

        public static ProductToken Generate()
        {
            return new ProductToken
            {
                id = Guid.NewGuid().ToString().Replace("-", ""),
                name = Guid.NewGuid().ToString().Replace('-', ' '),
                retail_price = 199F,
                images = new string[] { "http://i.ebayimg.com/00/s/MTAwMFgxMDAw/z/usYAAOSw-ndToGfO/$_57.JPG" },
                //Sale = SaleToken.Generate(1, 125).First()
                //size = "12cm x 14cm x 25cm",
                //weight = "0.8kg"
            };
        }

        public static readonly ProductToken Demo = new ProductToken { id = "000000000000000000000000" };
    }

    public static class ProductTokenExtensions
    {
        public static ProductToken AsToken(this Product product)
        {
            string[] sizes = product.size.Split('x');

            var token = new ProductToken
            {
                id = product.id,
                sku = product.sku,
                seller_id = product.seller_id,
                category = product.category,
                name = product.name,
                short_descritpion = product.short_descritpion,
                descritpion = product.descritpion,
                retail_price = product.retail_price / 100F,
                //purchase_price = product.purchase_price / 100F,
                //suggested_sell_price = product.suggested_sell_price / 100F,
                //images = product.images,
                length = double.Parse(sizes[0]),
                width = double.Parse(sizes[1]),
                height = double.Parse(sizes[2]),
                //size = product.size,
                weight = product.weight,
                //lead_time = product.lead_time,
                //is_draft = product.is_draft,
                //is_dropship = product.is_dropship,
                //is_oem = product.is_oem,
                //stock_model = product.stock_model,
                //min_quantity = product.min_quantity
                comments = product.comments == null ? Enumerable.Empty<CommentToken>() : product.comments.Take(5).Select(c => c.AsToken())
            };

            List<int> indexes = new List<int>();
            for (int i = 0; i < product.images.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(product.images[i]))
                {
                    indexes.Add(i);
                }
            }
            token.image_indexes = indexes.ToArray();

            return token;
        }

        public static ProductToken AsToken(this Product product, UserPreferences prefs)
        {
            var token = product.AsToken();
            token.UserPreferences = prefs.AsToken(token.id);

            return token;
        }

        public static ProductToken AsSimpleToken(this Product product)
        {
            var token = new ProductToken
            {
                id = product.id,
                name = product.name,
                category = product.category,
                short_descritpion = product.short_descritpion,
                sale_id = product.sale_id
            };

            List<int> indexes = new List<int>();
            for (int i = 0; i < product.images.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(product.images[i]))
                {
                    indexes.Add(i);
                }
            }
            token.image_indexes = indexes.ToArray();

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
                size = string.Join("x", product.length, product.width, product.height),
                weight = product.weight,
                is_draft = product.is_draft
            };
        }
    }
}
