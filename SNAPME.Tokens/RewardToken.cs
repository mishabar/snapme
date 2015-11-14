using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Tokens
{
    public class RewardToken
    {
        public RewardType type { get; set; }
        public int points { get; set; }
    }

    public enum RewardType
    {
        Purchase,
        DirectInvitation,
        PurchaseCommission,
        SpecialOffers
    }
}
