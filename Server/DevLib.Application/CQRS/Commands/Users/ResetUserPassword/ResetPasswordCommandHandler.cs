using DevLib.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Users.ResetUserPassword;

public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ResetUserPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(ResetUserPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, command.NewPassword);

        if (!result.Succeeded)
        {
            throw new Exception("Error resetting password: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
