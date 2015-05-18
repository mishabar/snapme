using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Models.Welcome;

namespace SNAPME.Web.Controllers
{
    public class WelcomeController : Controller
    {
        private IInvitationService _invitationService;

        public WelcomeController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        // GET: Welcome
        public ActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(Request["ref"]))
            {
                HttpCookie cookie = new HttpCookie ("ref", Request["ref"].Trim());
                cookie.Expires = DateTime.Now.AddMonths(6);
                Response.Cookies.Add(cookie);
            }

            return View("LandingPage");
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(RegisterForInvitationModel model)
        {
            if (ModelState.IsValid)
            { 
                string referer = null;
                if (Request.Cookies["ref"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["ref"].Value))
                {
                    referer = Request.Cookies["ref"].Value.Trim();
                }
                string newRef = _invitationService.AddToList(model.Email, referer);
                HttpCookie cookie = new HttpCookie("ref");
                cookie.Expires = new DateTime(1900, 1, 1);
                Response.Cookies.Add(cookie);

                return View("ThankYou", (object)newRef);
            }

            return View();
        }
    }
}