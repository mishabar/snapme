using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Sale
{
    public class DropPriceDialogModel
    {
        public string ProductName { get; set; }
        public string SaleID { get; set; }
        public double Drop { get; set; }
    }
}