using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Web.Models.Dialog;

namespace SNAPME.Web.Controllers
{
    public class DialogController : Controller
    {
        // GET: NewUser
        public PartialViewResult NewUser()
        {
            return PartialView("_NewUser");
        }

        // GET: Login
        public PartialViewResult Login(string error, string message)
        {
            return PartialView("_Login", new LoginDialogModel { Error = error, Message = message });
        }

        // GET: PrivacyPolicy
        public PartialViewResult PrivacyPolicy()
        {
            return PartialView("_PrivacyPolicy");
        }
    }
}