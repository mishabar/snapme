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
    [RoutePrefix("api/v1")]
    public class CommunityController : ApiController
    {
        private ICommunityService _communityService;

        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
        }


        [Route("communities"), HttpGet]
        public async Task<IHttpActionResult> ListCommunities()
        {
            return Ok(await _communityService.ListCommunities());
        }

        [Route("community/{name}"), HttpGet]
        public async Task<IHttpActionResult> GetCommunityDetails(string name)
        {
            return Ok(await _communityService.GetCommunity(name));
        }

    }
}
