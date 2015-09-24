using System.Web.Mvc;

namespace SNAPME.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.Routes.MapMvcAttributeRoutes();

            //context.MapRoute(
            //    "Admin_Products",
            //    "Admin/Products",
            //    new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}