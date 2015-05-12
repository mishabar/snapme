using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Welcome
{
    public class RegisterForInvitationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}