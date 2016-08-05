using SNAPME.BackOffice.Models.Api;
using SNAPME.Live.Services.Interfaces;
using SNAPME.Live.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SNAPME.BackOffice.Controllers.Api
{
    [RoutePrefix("api/v1")]
    //[Authorize]
    public class ProductsController : ApiController
    {
        private IProductService _productsService;

        public ProductsController(IProductService productsService)
        {
            _productsService = productsService;
        }

        [Route("categories")]
        public IHttpActionResult GetCategories()
        {
            return Ok(new object[] { new { id = 1, name = "Men" }, new { id = 2, name = "Women" }, new { id = 3, name = "Kids" }, new { id = 4, name = "Pets" }, 
                new { id = 5, name = "Home" }, new { id = 6, name = "Gadgets" }, new { id = 7, name = "Art" }, new { id = 8, name = "Food" }, new { id = 9, name = "Media" }, 
                new { id = 10, name = "Architecture" }, new { id = 11, name = "Travel & Destinations" }, new { id = 12, name = "Sports & Outdoors" }, new { id = 13, name = "DIY & Crafts" },
                new { id = 14, name = "Workspace" }, new { id = 15, name = "Cars & Vehicles" }, new { id = 16, name = "Other" }});
        }

        [Route("products/{category_id}")]
        public IHttpActionResult GetProducts(int category_id)
        {
            return Ok(new object[] { 
                new { 
                    id = 1, 
                    name = "Audi A3", 
                    description = "Third generation (Typ 8V; 2012–)", 
                    price = 38000F, 
                    images = new string[] { "http://images.cdn.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/audi-a3-uk-1_0.jpg?itok=F-3bwGkZ" },
                    snapbox =  new { likes = 13 }
                } ,
                new { 
                    id = 2, 
                    name = "Audi RS3", 
                    description = "Third generation (Typ 8V; 2012–)", 
                    price = 45000F, 
                    images = new string[] { "http://images.cdn.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/931111101636441600x1060.jpg?itok=3lMFhfFU" },
                    snapbox =  new { likes = 103 }
                } 
            });
        }

        [Route("product/{product_id}")]
        public IHttpActionResult GetProduct(int product_id)
        {
            return Ok(new
            {
                id = 1,
                name = "Audi A3",
                description = "Third generation (Typ 8V; 2012–)",
                category = 2,
                price = 38000F,
                images = new string[] { "http://images.cdn.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/audi-a3-uk-1_0.jpg?itok=F-3bwGkZ" },
                dimensions = new { width = 160, height = 145, length = 320, weight = 980 },
                shipping = new { address = "1 Audi Drive", zip = 7777, city = "Perth", state = "West Australia", country = "Australia" }
            });
        }

        [Route("product"), HttpPost]
        public async Task<IHttpActionResult> SaveProduct(FullProductToken token)
        {
            var result = await _productsService.SaveProduct(token);

            return Ok(new { id = result });
        }
    }
}
