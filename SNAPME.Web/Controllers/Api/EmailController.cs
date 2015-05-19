using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Helpers;
using SNAPME.Web.Models.Emails;

namespace SNAPME.Web.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class EmailController : ApiController
    {
        private IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [Route("email/test"), HttpGet]
        public IHttpActionResult SendTestEmail(string email)
        {
            var renderer = new ViewRenderer();
            string body = renderer.RenderViewToString("~/Views/Emails/_InvitationListWelcome.cshtml", (object)"DEMO");

            _emailService.Send(email, "Welcome to iiSnap", body);

            return Ok();
        }
    }
}
