using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Contracts;
using WebApplication3.Data;
using WebApplication3.DTO;
using WebApplication3.Model;

namespace WebApplication3.Repostiory
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _context;

        public UserRepository(IMapper mapper, MyDbContext context)
        {
           _mapper = mapper;
           _context = context;

            //var users = new List<User>()
            //{
            //    new User { Id = 1, Name = "Somtochukwu", Description = "Software Engineer" },
            //    new User { Id = 2, Name = "Emeka", Description = "Data Scientist" },
            //    new User { Id = 3,Name = "Emmanuel", Description = "Data Engineer"}
            //};

            //_context.Users.AddRange(users);
            //_context.SaveChanges();
        }

        public async Task<AddUserDTO> CreateUserAsync(AddUserDTO userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                Description = userDTO.Description
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<AddUserDTO>(user);

        }

        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            var userDtos = _mapper.Map<List<UserDTO>>(users);

            return userDtos;
        }
    }
}
