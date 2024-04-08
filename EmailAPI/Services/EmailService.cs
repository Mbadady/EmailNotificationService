using EmailAPI.Contracts;
using EmailAPI.Data;
using EmailAPI.DTO;
using EmailAPI.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogEmailRepository logEmailRepository;
        private readonly IEmailSender emailSender;

        public EmailService(ILogEmailRepository logEmailRepository, IEmailSender emailSender)
        {
            this.logEmailRepository = logEmailRepository;
            this.emailSender = emailSender;
        }
        public async Task EmailAndLogEmailAsync(AddUserDto addUserDto)
        {
            string subject = "Test Email";

            StringBuilder message = new StringBuilder();

            message.AppendLine("<h1>User's name and Description Requested</h1>");
            message.AppendLine("<br/> Name: " + addUserDto.Name);
            message.AppendLine("<br/> Description: " + addUserDto.Description);
            message.Append("<br/>");
            message.AppendLine("<br/> Thank you for using the service");
            message.Append("<br/>");

            await logEmailRepository.LogEmailAsync(message.ToString(), addUserDto.Email);
            await emailSender.SendEmailAsync(addUserDto.Email, subject, message.ToString());

        }
    }
}
