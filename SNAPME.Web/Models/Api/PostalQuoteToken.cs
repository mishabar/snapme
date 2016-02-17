using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Api
{
    public class PostalQuoteToken
    {
        [Required]
        public string id { get; set; }
        [Required]
        public int zip { get; set; }
    }
}