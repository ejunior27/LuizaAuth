using Domain.LuizaAuth.Configurations;
using Domain.LuizaAuth.DTOs;
using Domain.LuizaAuth.Exceptions;
using Domain.LuizaAuth.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Service.LuizaAuth.Services
{
    public class EmailService : IEmailService
    {       
        private const string MSG_ERRO_ENVIO_EMAIL = "Erro no envio do e-mail";

        public EmailSettings emailSettings { get; set; }
        public EmailService(IOptions<EmailSettings> options)
        {
            this.emailSettings = options.Value;
        }

        public Task SendEmail(UserDto userViewModel, string subject, string body)
        {
            try
            {   
                Execute(userViewModel, subject, body).Wait();
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"{MSG_ERRO_ENVIO_EMAIL}: {ex.Message}");
            }
        }       

        public async Task Execute(UserDto userViewModel, string subject, string body)
        {
            try
            {
                string toEmail = userViewModel.Email;

                var mail = new MailMessage()
                {
                    From = new MailAddress(emailSettings.UsernameEmail)
                };

                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                
                using (SmtpClient smtp = new SmtpClient(emailSettings.PrimaryDomain, emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(emailSettings.UsernameEmail, emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }        
    }
}
