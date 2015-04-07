using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class ProductToken
    {
        public string id { get; set; }
        [Required]
        public string seller_id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string short_descritpion { get; set; }
        public string descritpion { get; set; }
        [Required]
        [Range(0.01F, Double.MaxValue)]
        public double retail_price { get; set; }
        public string[] images { get; set; }
        [Required]
        public string size { get; set; }
        [Required]
        public string weight { get; set; }
        [Required]
        public ProductCondition condition { get; set; }
        public bool is_draft { get; set; }

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
                size = "12cm x 14cm x 25cm",
                weight = "0.8kg",
                condition = ProductCondition.New
            };
        }
    }

    public static class ProductTokenExtensions
    {
        public static ProductToken AsToken(this Product product)
        {
            return new ProductToken {
                id = product.id,
                seller_id = product.seller_id,
                name = product.name,
                short_descritpion = product.short_descritpion,
                descritpion = product.descritpion,
                retail_price = product.retail_price / 100F,
                images = product.images,
                size = product.size,
                weight = product.weight,
                condition = (ProductCondition)product.condition,
                is_draft = product.is_draft
            };
        }

        public static Product AsProduct(this ProductToken product)
        {
            return new Product
            {
                id = product.id,
                seller_id = product.seller_id,
                name = product.name,
                short_descritpion = product.short_descritpion,
                descritpion = product.descritpion,
                retail_price = (int)Math.Floor(product.retail_price * 100F),
                size = product.size,
                weight = product.weight,
                condition = (int)product.condition
                //images, is_draft -> Saved Separately
            };
        }
    }
}
