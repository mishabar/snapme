using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using SNAPME.Live.Services.Tokens;

namespace SNAPME.Live.Controllers.Api
{
    [RoutePrefix("api/v1/payment")]
    [Authorize]
    public class PaymentController : ApiController
    {
        [Route("customer/cards"), HttpGet]
        public async Task<IHttpActionResult> GetCustomerCards()
        {
            var userManager = Request.GetOwinContext().Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var customerIdClaim = user.Claims.Find(c => c.Type == "billing:customer_id");
            if (customerIdClaim != null)
            {
                var customerService = new StripeCustomerService();
                StripeCustomer stripeCustomer = customerService.Get(customerIdClaim.Value);

                return Ok(stripeCustomer.SourceList.Data.Select(c => new CardInfoToken(c)));
            }

            return Ok(new CardInfoToken[0]);
        }

        [Route("customer/{id}/charge"), HttpGet]
        public IHttpActionResult CreateCharge(string id)
        {
            var chargeService = new StripeChargeService();
            StripeChargeCreateOptions options = new StripeChargeCreateOptions
            {
                Amount = 500,
                Currency = "aud",
                CustomerId = id,
                Capture = false,
                Source = new StripeSourceOptions { TokenId = "card_17ng6XB0fmmxLkFSst9mgeTA" }
            };
            var stripeCharge = chargeService.Create(options);

            return Ok(stripeCharge);
        }
    }
}



//var xxx = Request.GetOwinContext().Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);