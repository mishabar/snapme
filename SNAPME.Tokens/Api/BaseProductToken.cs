using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Tokens.Api
{
    public class BaseProductToken
    {
        [Required]
        public string id { get; set; }
    }
}
