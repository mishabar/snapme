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
        public SaleState state { get; set; }
        public SaleType sale_type { get; set; }
        public int stock { get; set; }
        public int required_snaps { get; set; }
        public DateTime starts_on { get; set; }
        public DateTime ends_on { get; set; }

        public SaleToken()
        {
        }

        public SaleToken(SaleDetails sale)
            : this(sale, false)
        {
        }

        public SaleToken(SaleDetails sale, bool allSnaps)
        {
            sale_id = sale.sale_id;
            target = sale.target;
            current_price = sale.current_price;
            progress = sale.progress;
            snaps = sale.snaps.OrderByDescending(s => s.snapped_at).Take(allSnaps ? sale.snaps_count : 20).Select(s => new SnapToken(s)).ToArray();
            snaps_count = sale.snaps_count;
            state = sale.state;
            sale_type = sale.sale_type;
            required_snaps = sale.required_snaps;
            stock = sale.stock;
            starts_on = sale.starts_on;
            ends_on = sale.ends_on;
        }
    }
}
