using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNAPME.Live.Models.Api
{
    public class ShippingCalculationRequest
    {
        public string zip { get; set; }
        public string to_zip { get; set; }
        public float width { get; set; }
        public float length { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
    }
}