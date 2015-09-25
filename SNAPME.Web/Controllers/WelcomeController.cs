using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Helpers;
using SNAPME.Web.Models.Welcome;

namespace SNAPME.Web.Controllers
{
    public class WelcomeController : Controller
    {
        private IInvitationService _invitationService;
        private IEmailService _emailService;

        public WelcomeController(IInvitationService invitationService, IEmailService emailService)
        {
            _invitationService = invitationService;
            _emailService = emailService;
        }

        // GET: Welcome
        public ActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(Request["ref"]))
            {
                HttpCookie cookie = new HttpCookie ("iisref", Request["ref"].Trim());
                cookie.Expires = DateTime.Now.AddMonths(6);
                Response.Cookies.Add(cookie);
            }

            return View("Welcome");
        }

        [HttpPost]
        public ActionResult PreLaunch()
        {
            if (string.IsNullOrEmpty(Request["key"]))
            {
                return Redirect("/Welcome");
            }

            return View("PreLaunch", (object)Request["key"]);
        }

        public ActionResult ComingSoon()
        {
            if (!string.IsNullOrWhiteSpace(Request["ReturnUrl"])) 
            {
                return Redirect("/ComingSoon");
            }
            return View();
        }

        [HttpGet]
        public ActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Seller()
        {
            return View();
        }
    }
}