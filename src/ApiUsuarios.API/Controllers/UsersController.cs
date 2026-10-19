using ApiUsuarios.API.Models;
using ApiUsuarios.Application.Users.Commands.CreateUser;
using ApiUsuarios.Application.Users.Commands.DeleteUser;
using ApiUsuarios.Application.Users.Commands.UpdateUser;
using ApiUsuarios.Application.Users.Queries.GetAllUsers;
using ApiUsuarios.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRequest request)
    {
        try
        {
            var user = await sender.Send(new CreateUserCommand(request.Name, request.Email));

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await sender.Send(new GetAllUsersQuery());

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await sender.Send(new GetUserByIdQuery(id));

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserRequest request)
    {
        try
        {
            var user = await sender.Send(new UpdateUserCommand(id, request.Name, request.Email));

            return user is null ? NotFound() : NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await sender.Send(new DeleteUserCommand(id));

        return deleted ? NoContent() : NotFound();
    }
}
