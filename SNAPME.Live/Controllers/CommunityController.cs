using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Live.Controllers
{
    [Authorize]
    [RoutePrefix("community")]
    public class CommunityController : Controller
    {
        // GET: Community
        [Route("{community}", Name = "Community")]
        public ActionResult Index(string community)
        {
            return View(new Dictionary<string, string> { { "community", community } });
        }
    }
}