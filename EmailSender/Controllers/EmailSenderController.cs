using EmailSender.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly IConfiguration _configuration;

        public EmailSenderController(IEmailSender emailSender, HtmlEncoder htmlEncoder, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _htmlEncoder = htmlEncoder;
            _configuration = configuration;
        }

        [HttpPost("send-email")]
        public async Task<object> SendEmail(EmailDTO emailDTO)
        {
            try
            {
                await this._emailSender.SendEmailAsync(
                    _htmlEncoder.Encode(_configuration["AppSettings:ReceivingEmail"]),
                    _htmlEncoder.Encode(emailDTO.Subject),
                    _htmlEncoder.Encode($"{emailDTO.Email} - {emailDTO.Body}")
                 );
                return new
                {
                    OK = true,
                };

            }
            catch(Exception ex)
            {
                return new
                {
                    OK = false
                };
            }
           
        }

       
    }
}
