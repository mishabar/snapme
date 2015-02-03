using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Web.Core
{
    public static class Extensions
    {
        public static string AsPrice(this int price)
        {
            return ((decimal)price / 100M).ToString("C");
        }

        public static string AsTimeToEnd(this DateTime dt)
        {
            var timeLeft = (dt - DateTime.UtcNow);
            return string.Format("{0:00}:{1:00}:{2:00}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
        }
    }
}
