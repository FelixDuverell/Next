using AutoMapper;
using backend.Dto.Users;
using backend.Models;
using static backend.Controllers.AuthController;

namespace backend.Profiles;

public class UserProfile : Profile 
{

    public UserProfile()
    {

        CreateMap<AppUser, UserResponseDto>();
        CreateMap<CreateUserRequestDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();

    }
}
