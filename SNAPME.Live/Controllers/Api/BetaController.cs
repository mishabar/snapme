using SNAPME.Live.Models.Api;
using SNAPME.Live.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SNAPME.Live.Controllers.Api
{
    [RoutePrefix("api/v1/beta")]
    public class BetaController : ApiController
    {
        private IBetaRegistrationService _service;

        public BetaController(IBetaRegistrationService service)
        {
            _service = service;
        }


        [Route("register"), HttpPost]
        public async Task<IHttpActionResult> Register(BetaRegistrationRequest request)
        {
            if (ModelState.IsValid)
            {
                int result = await _service.Register(request.email);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }
    }
}
