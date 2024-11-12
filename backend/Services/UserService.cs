using AutoMapper;
using backend.Dto.Users;
using backend.Models;
using backend.Repository;

namespace backend.Services;

public interface IUserService
{

    UserResponseDto? GetById(int id);

    UserResponseDto CreateNewUser(CreateUserRequestDto userDto);

}

public class UserService : IUserService
{

    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;

    public UserService(IUserRepo userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public UserResponseDto? GetById(int id)
    {
        AppUser? user = _userRepo.GetUserById(id);
        return _mapper.Map<UserResponseDto>(user);
    }

    public UserResponseDto CreateNewUser(CreateUserRequestDto userDto)
    {
        AppUser user = _mapper.Map<AppUser>(userDto);
        AppUser userCreated = _userRepo.SaveNew(user);
        return _mapper.Map<UserResponseDto>(userCreated);
    }
}