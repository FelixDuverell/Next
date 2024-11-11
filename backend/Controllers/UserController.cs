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
    public UserController(BackendContext db)
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponseDto> GetById([FromRoute] int id)
    {
        User? user = _db.Users.Find(id);
        if (user == null)
            return NotFound();

        UserResponseDto userDto = new UserResponseDto(user.Id, user.Password);
        return Ok(userDto);
    }

    [HttpPost]
    public ActionResult<UserResponseDto> CreateUser([FromBody] CreateUserRequestDto userDto)
    {
        User user = new User(userDto.Username, userDto.Passowrd);
        _db.Users.Add(user);
        _db.SaveChanges();
        return new UserResponseDto(user.Id, user.Username);
    }

}