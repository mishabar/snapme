using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Tokens
{
    public class SaleToken
    {
        public string id { get; set; }
        public DateTime started_on { get; set; }
        public DateTime ends_on { get; set; }
        public int current_price { get; set; }
        public int target_price { get; set; }
        public ProductToken product { get; set; }

        public static IEnumerable<SaleToken> Generate(int count)
        {
            List<SaleToken> list = new List<SaleToken>();
            Random random = new Random(DateTime.UtcNow.Millisecond);
            for (int i = 0; i < count; i++)
            {
                list.Add(new SaleToken { 
                    id = Guid.NewGuid().ToString(), 
                    started_on = DateTime.UtcNow.AddMinutes(-random.Next(400)),
                    current_price = random.Next(2000) + 8000,
                    target_price = -random.Next(2000) + 8000,
                    ends_on = DateTime.UtcNow.AddMinutes(random.Next(400) - 200),
                    product = ProductToken.Generate()
                });
            }

            return list;
        }
    }
}
