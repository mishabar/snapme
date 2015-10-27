using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens.Admin;

namespace SNAPME.Web.Controllers.Api
{
    [RoutePrefix("api/v1")]
    [Authorize]
    public class SellerController : ApiController
    {
        private ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [Route("seller/{id}"), HttpGet, Authorize]
        public IHttpActionResult Get(string id)
        {
            return Ok(_sellerService.GetById(id));
        }

        [Route("save/seller")]
        public IHttpActionResult Create(SellerToken token)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _new = string.IsNullOrWhiteSpace(token.id);
                    _sellerService.Save(token);
                    return Ok(new { admin = User.IsInRole("Administrator"), isNew = _new });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
