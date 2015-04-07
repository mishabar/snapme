using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Areas.Admin.Models
{
    public class ProductImageModel
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string image { get; set; }
        [Required]
        public int idx { get; set; }
    }
}