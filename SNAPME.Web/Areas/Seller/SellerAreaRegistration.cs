using System.Web.Mvc;

namespace SNAPME.Web.Areas.Seller
{
    public class SellerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Seller";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Seller_Register",
                url: "Seller/Register",
                defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "SNAPME.Web.Areas.Seller.Controllers" }
            );

            context.MapRoute(
                name: "Seller_Registered",
                url: "Seller/Registered",
                defaults: new { controller = "Home", action = "Registered", id = UrlParameter.Optional },
                namespaces: new string[] { "SNAPME.Web.Areas.Seller.Controllers" }
            );

            context.MapRoute(
                name: "Seller_default",
                url: "Seller/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SNAPME.Web.Areas.Seller.Controllers" }
            );
        }
    }
}