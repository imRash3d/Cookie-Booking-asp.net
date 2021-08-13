﻿
using CookieBooking.Dtos;
using CookieBooking.Entities;
using CookieBooking.Extensions;
using CookieBooking.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public  class MailService: IMailService
    {
        private readonly DbContextService _context;
        private readonly IConfiguration _configuration;
       // private readonly ILogger _logger;
        public MailService(DbContextService context, IConfiguration configuration  )
        {
            _context = context;
            _configuration = configuration;
           // _logger = logger;
        }
        public void SendMail(EmailModelDto emailDataDto)
        {
            MailConfiguration mailConfiguration = _context.MailConfigurations.SingleOrDefault(x => x.MailConfigurationId == _configuration["MailConfigurationId"]);
            
            EmailTemplate emailTemplate = _context.EmailTemplates.SingleOrDefault(x => x.TemplateName == emailDataDto.EmailTemplateName);


            if (emailDataDto != null && mailConfiguration!=null && emailTemplate!=null)
            {
                string to = emailDataDto.To; //To address    
                string from = mailConfiguration.MailSenderAddress; //From address    
                MailMessage message = new MailMessage(from, to);
                var options = new JsonSerializerOptions
                {
                    IncludeFields = true,
                };
                string mailbody = Regex.Unescape(emailTemplate.TemplateBody);      // unescape  Mailtemaplate 



                // formate Mailtemaplate with place holder 

                Regex re = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);
                string outputMailBody = re.Format(mailbody, emailDataDto.DataContext);



                message.Subject = emailTemplate.TemplateSubject;
                message.Body = outputMailBody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(mailConfiguration.Host, mailConfiguration.Port); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(mailConfiguration.MailSenderUserName, mailConfiguration.MailAccountPassword);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                   // _logger.LogError(ex.Message);
                }
            }

        }
    }
}
