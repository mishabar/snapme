using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Tokens
{
    public class CategoryToken
    {
        public string id { get; set; }
        public string name { get; set; }
        public IEnumerable<SaleToken> sales { get; set; }

        public static IEnumerable<CategoryToken> Generate(int count)
        {
            Random random = new Random(DateTime.UtcNow.Millisecond);

            string[] names = new string[] { "Electronics", "Cosmetics", "Fashion", "Baby Products", "Homeware" };
            List<CategoryToken> list = new List<CategoryToken>();
            for (int i = 0; i < count; i++)
			{
                list.Add(new CategoryToken { id = Guid.NewGuid().ToString(), name = names[i], sales = SaleToken.Generate(random.Next(5) + 1) });
			}
            return list;
        }
    }
}
