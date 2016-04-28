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
using SNAPME.Live.Services.Tokens;
using SNAPME.Live.Data.Entities;

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
                FullProductToken product = null;
                long productId = 0;
                if (long.TryParse(name, out productId))
                {
                    product = await _productService.GetProduct(productId);
                }
                else
                {
                    product = await _productService.GetProduct(name);
                }
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

        [Authorize(Roles="iiAdmin"), Route("products"), HttpGet]
        public async Task<IHttpActionResult> GetProducts()
        {
            return Ok(await _productService.GetProducts());
        }

        [Authorize(Roles = "iiAdmin"), Route("product"), HttpPost]
        public async Task<IHttpActionResult> UpdateProduct(FullProductToken request)
        {
            return Ok(await _productService.UpdateProduct(request));
        }

        [Authorize(Roles = "iiAdmin"), Route("product/{id}/sales"), HttpGet]
        public async Task<IHttpActionResult> GetProductSales(long id)
        {
            return Ok(await _productService.GetSales(id));
        }

        [Authorize(Roles = "iiAdmin"), Route("product/{id}/sales"), HttpPost]
        public async Task<IHttpActionResult> SaveSale(SaveSaleRequest request)
        {
            return Ok(await _productService.SaveSale(request.product_id, new SaleToken{ 
                sale_id = request.sale_id,
                starts_on = request.starts_on,
                ends_on = request.ends_on,
                sale_type = request.sale_type,
                stock = request.stock,
                required_snaps = request.required_snaps,
                target = request.target,
                state = SaleState.Future
            }));
        }

        [Route("sale/join"), HttpPost, Authorize]
        public async Task<IHttpActionResult> JoinSale(JoinSaleRequest request)
        {
            // Verify existing customer on Stripe
            var userManager = Request.GetOwinContext().Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var customerIdClaim = user.Claims.Find(c => c.Type == "billing:customer_id");
            string customerId = null;
            string sourceId = request.source_id;

            if (customerIdClaim == null)
            {
                // create customer
                var data = await _paymentService.CreateCustomer(user.Email,
                    string.Format("{0}, {1}/{2}", user.UserName, user.Logins.First().LoginProvider, user.Logins.First().ProviderKey),
                    request.stripe_token);
                customerId = data[0];
                sourceId = data[1];

                if (customerId == null)
                    return Ok(new { error = new { message = "Error registering payment" } });

                await userManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("billing:customer_id", customerId));
            }
            else
            {
                customerId = customerIdClaim.Value;
            }

            string chargeId = await _paymentService.CreateCharge(customerId, sourceId, 
                (int)Math.Round(100F * (request.price * request.quantity + request.shipping_price), 0),
                string.Format("{0} plus Shipping", request.product_id));

            var snap = await _productService.JoinSale(request.product_id, User.Identity.GetUserId(), User.Identity.GetUserName(), customerId, chargeId,
                request.first_name, request.last_name, request.address, request.address2, request.city, request.state, request.zip_code);

            return Ok(snap);
        }
    }
}
