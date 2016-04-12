using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class SaleToken
    {
        public long sale_id { get; set; }
        public double target { get; set; }
        public double current_price { get; set; }
        public int progress { get; set; }
        public SnapToken[] snaps { get; set; }
        public int snaps_count { get; set; }

        public SaleToken(SaleDetails sale)
        {
            sale_id = sale.sale_id;
            target = sale.target;
            current_price = sale.current_price;
            progress = sale.progress;
            snaps = sale.snaps.OrderByDescending(s => s.snapped_at).Take(20).Select(s => new SnapToken(s)).ToArray();
            snaps_count = sale.snaps_count;
        }
    }
}
