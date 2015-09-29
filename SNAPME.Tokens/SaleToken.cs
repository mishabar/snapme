using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class SaleToken
    {
        public string id { get; set; }
        [Required]
        public string product_id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string starts_on { get; set; }
        public double retail_price { get; set; }
        [Required]
        public double target { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public int duration { get; set; }
        public double price { get; set; }
        public int drops { get; set; }
        public int progress { get; set; }
        [Required]
        public bool is_featured { get; set; }
        [Required]
        public int points { get; set; }
    }

    public static class SaleTokenExtensions
    {
        public static SaleToken AsToken(this Sale sale)
        {
            return new SaleToken 
            {
                id = sale.id, 
                product_id = sale.product_id,
                retail_price = (double)sale.retail_price / 100F,
                target = (double)sale.target / 100F,
                price = (double)sale.price / 100F,
                starts_on = sale.starts_on.ToString("dd/MM/yyyy"),
                quantity = sale.quantity,
                duration = sale.duration,
                drops = sale.drops.Count(),
                progress = sale.progress,
                is_featured = sale.is_featured,
                points = sale.points,
                name = sale.name,
                summary = sale.summary
            };
        }

        public static Sale AsSale(this SaleToken sale)
        {
            return new Sale
            {
                id = sale.id,
                product_id = sale.product_id,
                retail_price = (int)(sale.retail_price * 100),
                target = (int)(sale.target * 100),
                price = (int)(sale.price * 100),
                starts_on = DateTime.ParseExact(sale.starts_on, "dd/MM/yyyy", null),
                quantity = sale.quantity,
                duration = sale.duration,
                progress = sale.progress,
                is_featured = sale.is_featured,
                points = sale.points,
                summary = sale.summary
            };
        }
    }
}
