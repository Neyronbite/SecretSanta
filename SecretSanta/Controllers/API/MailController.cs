using SecretSanta.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecretSanta.Controllers.API
{
    public class MailController : ApiController
    {
        IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        // POST: api/Mail
        public IHttpActionResult Post(int listID)
        {
            string error = _mailService.SendMails(listID);
            if (error == null)
            {
                return Ok();
            }

            ModelState.AddModelError("validationError", error);
            return BadRequest(ModelState);
        }
    }
}
