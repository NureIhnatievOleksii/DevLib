using MediatR;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.UserAggregate;
using DevLib.Application.CQRS.Commands.Admins.BanUser;

namespace DevLib.Application.CQRS.Commands.Admin.BanUser;

public class BanUserCommandHandler(UserManager<User> userManager) : IRequestHandler<BanUserCommand, bool>
{
    public async Task<bool> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user == null)
        {
            return false; 
        }

        user.IsBanned = request.IsBanned;
        var result = await userManager.UpdateAsync(user);

        return result.Succeeded;
    }
}
