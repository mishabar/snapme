using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Live.Models.Api
{
    public class BetaRegistrationRequest
    {
        [Required]
        public string email { get; set; }
    }
}