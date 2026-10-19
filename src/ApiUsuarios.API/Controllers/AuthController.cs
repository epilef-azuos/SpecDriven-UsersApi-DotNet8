using ApiUsuarios.Application.Auth.Commands.Login;
using ApiUsuarios.Application.DTOs;
using ApiUsuarios.Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            var user = await sender.Send(new RegisterUserCommand(request.Name, request.Email, request.Password, request.Role));

            return CreatedAtAction("GetById", "Users", new { id = user.Id }, user);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await sender.Send(new LoginCommand(request.Email, request.Password));

            return Ok(response);
        }
        catch (UnauthorizedAccessException exception)
        {
            return Unauthorized(new { error = exception.Message });
        }
    }
}
