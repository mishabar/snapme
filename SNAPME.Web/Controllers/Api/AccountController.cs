using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Helpers;
using Microsoft.AspNet.Identity;
using SNAPME.Tokens;

namespace SNAPME.Web.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/v1")]
    public class AccountController : ApiController
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [Route("user"), HttpGet]
        public IHttpActionResult UserDetails()
        {
            var details = _accountService.Get(User.Identity.GetUserId());
            if (string.IsNullOrWhiteSpace(details.first_name))
            {
                var nameParts = User.Identity.DisplayName().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                details.first_name = nameParts[0];
                if (nameParts.Length > 1)
                {
                    details.last_name = string.Join(" ", nameParts.Skip(1).ToArray());
                }
            }
            return Ok(details);
        }

        [Route("user"), HttpPost]
        public IHttpActionResult SaveUserDetails(UserDetailsToken token)
        {
            token.id = User.Identity.GetUserId();
            token = _accountService.Save(token);
            return Ok(token);
        }

        [Route("user/address"), HttpPost]
        public IHttpActionResult SaveAddress(SNAPME.Web.Models.Api.Account.AddressToken address)
        {
            var addressToken = new AddressToken
            {
                name = address.name,
                line1 = address.line1,
                line2 = address.line2,
                city = address.city,
                state = address.state,
                zip_code = address.zip_code
            };
            if (address.idx == -1)
            {
                return Ok(_accountService.AddAddress(User.Identity.GetUserId(), addressToken));
            }
            else
            {
                return Ok(_accountService.UpdateAddress(User.Identity.GetUserId(), address.idx, addressToken));
            }
        }

        [Route("user/address/{idx}"), HttpDelete]
        public IHttpActionResult DeleteAddress(int idx)
        {
            return Ok(_accountService.DeleteAddress(User.Identity.GetUserId(), idx));
        }

        [Route("user/favorites"), HttpGet]
        public IHttpActionResult ListFavorites()
        {
            return Ok(_accountService.GetFavorites(User.Identity.GetUserId()));
        }

        [Route("user/snaps"), HttpGet]
        public IHttpActionResult ListSnaps()
        {
            return Ok(_accountService.GetSnaps(User.Identity.GetUserId()));
        }
    }
}
