﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Tokens
{
    public class ProductToken
    {
        public string id { get; set; }
        public string name { get; set; }
        public int retail_price { get; set; }
        public IEnumerable<string> images { get; set; }
        public string size { get; set; }
        public string weight { get; set; }
        public ProductCondition condition { get; set; }

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
}