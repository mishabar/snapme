using System;
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

        public static ProductToken Generate()
        {
            return new ProductToken { 
                id = Guid.NewGuid().ToString(),
                name = Guid.NewGuid().ToString().Replace('-', ' '), 
                retail_price = new Random().Next(5000) + 10000 };
        }
    }
}
