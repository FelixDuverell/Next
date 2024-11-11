using AutoMapper;
using backend.Dto.Users;
using backend.Models;

namespace backend.Profiles;

public class UserProfile : Profile 
{

    public UserProfile()
    {

        CreateMap<AppUser, UserResponseDto>();

    }
}
