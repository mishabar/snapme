using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Api
{
    public class ContactRequestToken
    {
        [Required]
        public string type { get; set; }
        [Required]
        public string name { get; set; }
        [Required, EmailAddress]
        public string email { get; set; }
        public string details { get; set; }
    }
}