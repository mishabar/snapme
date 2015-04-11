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
        public double current_price { get; set; }
        public int progress { get; set; }
        public DateTime? ended_on { get; set; }
        public int target_price { get; set; }

        public int potential_points { get; set; }

        public static IEnumerable<SaleToken> Generate(int count, int purchase_price)
        {
            List<SaleToken> list = new List<SaleToken>();
            Random random = new Random(DateTime.UtcNow.Millisecond);
            int target_price = (int)(purchase_price * 1.25);

            for (int i = 0; i < count; i++)
            {
                list.Add(new SaleToken { 
                    id = Guid.NewGuid().ToString(), 
                    started_on = DateTime.UtcNow.AddMinutes(-random.Next(400)),
                    current_price = random.NextDouble() * 1000F,
                    progress = random.Next(95),
                    ends_on = DateTime.UtcNow.AddMinutes(random.Next(400) - 200),
                    target_price = target_price,
                    ended_on = null,
                    potential_points = random.Next(150)
                });
            }

            return list;
        }
        public static IEnumerable<SaleToken> GenerateEnded(int count)
        {
            List<SaleToken> list = new List<SaleToken>();
            Random random = new Random(DateTime.UtcNow.Millisecond);
            for (int i = 0; i < count; i++)
            {
                DateTime ends_on = DateTime.UtcNow.AddMinutes(random.Next(200) - 400); 
                list.Add(new SaleToken
                {
                    id = Guid.NewGuid().ToString(),
                    started_on = DateTime.UtcNow.AddMinutes(-random.Next(400)),
                    current_price = random.Next(2000) + 8000,
                    //target_price = -random.Next(2000) + 8000,
                    ends_on = ends_on,
                    ended_on = ends_on
                });
            }

            return list;
        }
    }
}
