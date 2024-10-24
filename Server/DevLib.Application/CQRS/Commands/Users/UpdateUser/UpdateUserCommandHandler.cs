using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.CQRS.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(command.UserId.ToString())
                       ?? throw new Exception("User not found");

            user.Email = command.Email;
            user.UserName = command.UserName;
            user.Photo = command.Photo;

            await userManager.UpdateAsync(user);
        }
    }
}
