﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Web.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Index()
        {
            return View(new Random().Next(2) == 0 ? "ComingSoon" : "LaunchingSoon");
        }
    }
}