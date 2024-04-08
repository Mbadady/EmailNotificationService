using EmailAPI.Contracts;
using EmailAPI.Data;
using EmailAPI.DTO;
using EmailAPI.Model;
using System.Net.Mail;

namespace EmailAPI.Repository
{
    public class LogEmailRepository : ILogEmailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LogEmailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }
        public async Task<bool> LogEmailAsync(string message, string emailAddress)
        {
            try
            {
                EmailLogger emailLog = new EmailLogger()
                {
                    Message = message,
                    Email = emailAddress,
                    EmailSent = DateTime.UtcNow
                };

                await _dbContext.EmailLoggers.AddAsync(emailLog);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
