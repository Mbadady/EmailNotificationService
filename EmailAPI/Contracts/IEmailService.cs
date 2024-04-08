using EmailAPI.DTO;

namespace EmailAPI.Contracts
{
    public interface IEmailService
    {

        Task EmailAndLogEmailAsync(AddUserDto addUserDto);
    }
}
