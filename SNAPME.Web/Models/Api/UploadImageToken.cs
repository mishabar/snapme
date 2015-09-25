using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Api
{
    public class UploadImageToken
    {
        [Required]
        public string id { get; set; }
        [Required]
        [Range(0, 6)]
        public int idx { get; set; }
        [Required]
        public string stream { get; set; }
    }
}