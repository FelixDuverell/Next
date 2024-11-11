using AutoMapper;
using backend.Data;
using backend.Dto.Users;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;


[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{

    private readonly BackendContext _db;
    private readonly IMapper _mapper;
    public UserController(BackendContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponseDto> GetById([FromRoute] int id)
    {
        AppUser? user = _db.AppUsers.Find(id);
        if (user == null)
            return NotFound();

        UserResponseDto userDto = _mapper.Map<UserResponseDto>(user);
        return Ok(userDto);
    }

    [HttpPost]
    public ActionResult<UserResponseDto> CreateUser([FromBody] CreateUserRequestDto userDto)
    {
        AppUser user = new(userDto.Username, userDto.Passowrd);
        _db.AppUsers.Add(user);
        _db.SaveChanges();
        return new UserResponseDto(user.Id, user.Username);
    }

}