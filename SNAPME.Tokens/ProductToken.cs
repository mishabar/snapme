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
        public string[] images { get; set; }
        [Required]
        public string length { get; set; }
        [Required]
        public string width { get; set; }
        [Required]
        public string height { get; set; }
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
                //size = "12cm x 14cm x 25cm",
                weight = "0.8kg",
                condition = ProductCondition.New
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
                seller_id = product.seller_id,
                category = product.category,
                name = product.name,
                short_descritpion = product.short_descritpion,
                descritpion = product.descritpion,
                retail_price = product.retail_price / 100F,
                purchase_price = product.purchase_price / 100F,
                images = product.images,
                length = sizes[0],
                width = sizes[1],
                height = sizes[2],
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
                purchase_price = (int)Math.Floor(product.purchase_price * 100F),
                size = string.Join("x", product.length, product.width, product.height),
                weight = product.weight,
                condition = (int)product.condition
                //images, is_draft -> Saved Separately
            };
        }
    }
}
