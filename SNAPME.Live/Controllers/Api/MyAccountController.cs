using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SNAPME.Live.Controllers.Api
{
    [Authorize]
    public class MyAccountController : ApiController
    {

        public async Task<IHttpActionResult> GetCommunities()
        {
            return Ok();
        }
    }
}
