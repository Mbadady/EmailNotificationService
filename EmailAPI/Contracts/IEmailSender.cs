using EmailAPI.DTO;

namespace EmailAPI.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
