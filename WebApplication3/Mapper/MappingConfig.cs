using AutoMapper;
using WebApplication3.DTO;
using WebApplication3.Model;

namespace WebApplication3.Mapper
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<User, AddUserDTO>().ReverseMap();

        }
    }
}
