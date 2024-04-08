using EmailAPI.DTO;

namespace EmailAPI.Contracts
{
    public interface ILogEmailRepository
    {
        Task<bool> LogEmailAsync(string message, string emailAddress);
    }
}
