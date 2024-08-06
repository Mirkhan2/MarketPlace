using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.App.Services.Interfaces;

namespace MarketPlace.App.Services.Implementations
{
    public class EmailService : IEmailService
    {
        #region constractor
        private readonly ISiteService _siteService;
        public EmailService(ISiteService siteService)
        {
              _siteService = siteService;
        }
        #endregion
        public async void SendEmail(string to, string subject, string body)
        {
            var defaultSiteEmail = await _siteService.GetDefaultEmail();

            MailMessage mail = new MailMessage();

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(defaultSiteEmail.From, defaultSiteEmail.DisplayName);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            if (defaultSiteEmail.Port != 0)
            {
                SmtpServer.Port = defaultSiteEmail.Port;
                SmtpServer.EnableSsl = defaultSiteEmail.EnableSSL;
            }

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultSiteEmail.From, defaultSiteEmail.Password);
            SmtpServer.Send(mail);
        }
    }
}
