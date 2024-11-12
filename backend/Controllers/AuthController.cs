using AutoMapper;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public record RegisterDto(string Email, string Username, string Password);

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto dto)
    {
        if(dto == null)
            return BadRequest();
        AppUser user = _mapper.Map<AppUser>(dto);
        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        else
        {
            return Created();
        }
    }

    public record LoginDto(string Username, string Password);

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
       _signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

       var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

        if(!result.Succeeded)
        {
            return Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
        }
        else
        {
            return NoContent();
        }
    }
}