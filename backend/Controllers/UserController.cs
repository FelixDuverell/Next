using AutoMapper;
using backend.Data;
using backend.Dto.Users;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;


[ApiController]
[Route("api/users")]
[Authorize(Roles = "admin")]
public class UserController : ControllerBase
{

    // private readonly BackendContext _db;
    // private readonly IMapper _mapper;

    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        // _db = db;
        // _mapper = mapper;
        _userService = userService;
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponseDto> GetById([FromRoute] int id)
    {
        UserResponseDto? user = _userService.GetById(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<UserResponseDto> CreateUser([FromBody] CreateUserRequestDto userDto)
    {
        return Ok (_userService.CreateNewUser(userDto));
    }

}