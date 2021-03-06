﻿using ECommerceIServices;
using ECommerceModels.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServices
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthSenderOptions Options { get; }

        public async Task<Response> Execute(string apiKey, string email, string subject, string message)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("shaven999@gmail.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
           
            msg.SetClickTracking(false, false);

            return await client.SendEmailAsync(msg);
        }

        public async Task<Response> SendEmailAsync(string email, string subject, string message)
        {
            return await Execute(Options.SendGridKey, email, subject, message);
        }
    }
}
