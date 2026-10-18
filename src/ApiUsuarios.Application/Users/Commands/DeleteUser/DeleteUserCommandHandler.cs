using ApiUsuarios.Domain.Interfaces;
using MediatR;

namespace ApiUsuarios.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetById(request.Id);

        if (user is null)
        {
            return false;
        }

        await repository.Delete(user);

        return true;
    }
}
