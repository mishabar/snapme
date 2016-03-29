using SNAPME.Live.Models.Api;
using SNAPME.Live.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace SNAPME.Live.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class ProductController : ApiController
    {
        private IProductService _productService;
        private IPaymentService _paymentService;

        public ProductController(IProductService productService, IPaymentService paymentService)
        {
            _productService = productService;
            _paymentService = paymentService;
        }

        [Route("product/{name}"), HttpGet]
        public async Task<IHttpActionResult> GetProduct(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var product = await _productService.GetProduct(name);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(product);
                }
            }

            return BadRequest("Missing Product Name");
        }

        [Route("sale/join"), HttpPost, Authorize]
        public async Task<IHttpActionResult> JoinSale(JoinSaleRequest request)
        {
            // Verify existing customer on Stripe
            var userManager = Request.GetOwinContext().Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var customerIdClaim = user.Claims.Find(c => c.Type == "billing:customer_id");
            string customerId = null;

            if (customerIdClaim == null)
            {
                // create customer
                customerId = await _paymentService.CreateCustomer(user.Email,
                    string.Format("{0}, {1}/{2}", user.UserName, user.Logins.First().LoginProvider, user.Logins.First().ProviderKey),
                    request.stripe_token);
                if (customerId == null)
                    return Ok(new { error = new { message = "Error registering payment" } });

                await userManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("billing:customer_id", customerId));
            }
            else 
            {
                customerId = customerIdClaim.Value;
            }



            return Ok();
        }
    }
}
