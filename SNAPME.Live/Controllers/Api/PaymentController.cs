using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNAPME.Live.Controllers.Api
{
    [RoutePrefix("api/v1/payment")]
    [Authorize]
    public class PaymentController : ApiController
    {
        [Route("customer/{id}/cards"), HttpGet]
        public IHttpActionResult GetCustomerCards(string id)
        {
            var customerService = new StripeCustomerService();
            StripeCustomer stripeCustomer = customerService.Get(id);

            return Ok(stripeCustomer.SourceList.Data);
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