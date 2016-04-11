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
        public async Task<IHttpActionResult> MyCommunities()
        {
            try
            {
                return Ok(await _communityService.ListMyCommunities());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("communities/list"), HttpGet]
        public async Task<IHttpActionResult> ListCommunities()
        {
            try
            {
                return Ok(await _communityService.ListCommunities());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("community/{name}"), HttpGet]
        public async Task<IHttpActionResult> GetCommunityDetails(string name)
        {
            return Ok(await _communityService.GetCommunity(name));
        }

    }
}
