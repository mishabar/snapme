using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Helpers;
using SNAPME.Web.Models.Api;

namespace SNAPME.Web.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class PreLaunchController : ApiController
    {
        private IInvitationService _invitationService;
        private IEmailService _emailService;

        public PreLaunchController(IInvitationService invitationService, IEmailService emailService)
        {
            _invitationService = invitationService;
            _emailService = emailService;
        }

        [Route("prelaunch/register"), HttpPost]
        public IHttpActionResult Register(PreLaunchRegisterToken token) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string refCode = _invitationService.AddToList(token.Email, token.Referrer);

                    var renderer = new ViewRenderer();
                    string body = renderer.RenderViewToString("~/Views/Emails/_PreLaunch.cshtml", (object)refCode);

                    _emailService.Send(token.Email, "iiSnap - Thank you for your interest", body, new string[] { "~/Content/Images/logo.png" });

                    return Ok(refCode);
                }
                catch(Exception ex)
                { 
                    return InternalServerError(ex);
                }
            }

            return BadRequest(ModelState);
        }
    }
}