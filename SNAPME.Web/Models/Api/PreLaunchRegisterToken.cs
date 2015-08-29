using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Api
{
    public class PreLaunchRegisterToken
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}