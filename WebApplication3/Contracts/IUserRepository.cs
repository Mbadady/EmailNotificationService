using WebApplication3.DTO;

namespace WebApplication3.Contracts
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAll();

        Task<AddUserDTO> CreateUserAsync(AddUserDTO addUserDTO);
    }
}
