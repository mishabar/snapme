using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Models.Api;

namespace SNAPME.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/v1")]
    public class ContactController : ApiController
    {
        private IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("contact")]
        public IHttpActionResult ContactRequest(ContactRequestToken token)
        {
            _emailService.Send(ConfigurationManager.AppSettings["contactEmail"], GetSubjectFromType(token.type), GetBodyFromToken(token));
            return Ok();
        }

        private string GetBodyFromToken(ContactRequestToken token)
        {
            if (token.type.ToLowerInvariant() == "seller")
            {
                return string.Format("<div><strong>Name:</strong> {0}</div><div><strong>Email:</strong> {1}</div><div><strong>Details:</strong> {2}</div>", token.name, token.email, token.details.Replace("\n", "<br/>"));
            }
            else if (token.type.ToLowerInvariant() == "user")
            {
                return string.Format("Please contant {0}, at {1} regarding: <br/>{2}", token.name, token.email, token.details.Replace("\n", "<br/>"));
            }

            throw new NotSupportedException("Contact type not supported");
        }

        private string GetSubjectFromType(string type)
        {
            if (type.ToLowerInvariant() == "seller")
            {
                return "Please contact this seller regarding cooperation";
            }
            else if (type.ToLowerInvariant() == "user")
            {
                return "Please contact this user regarding a question";
            }

            throw new NotSupportedException("Contact type not supported");
        }
    }
}
