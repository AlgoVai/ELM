using ELearningWeb.Helper;
using ELearningWeb.IRepository;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace ELearningWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
             _emailService = emailService;
        }
        [HttpPost]
        [Route("EmailSend")]
        public async Task<IActionResult> SendEmail(MailRequest _mailRequest)
        {
            try
            {
                MailRequest mRequest = new MailRequest();
                mRequest.ToEmail = _mailRequest.ToEmail;
                mRequest.Subject = _mailRequest.Subject;
                mRequest.Body = _mailRequest.Body;
                await _emailService.SendEmailAsync(mRequest);


                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
