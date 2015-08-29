using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNAPME.Web.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class PreLaunchController : ApiController
    {
        [Route("prelaunch/register"), HttpPost]
        public IHttpActionResult Register() 
        {
            return Ok(Request.Headers.Referrer);
        }
    }
}
