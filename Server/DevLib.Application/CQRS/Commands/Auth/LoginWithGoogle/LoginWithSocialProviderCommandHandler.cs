using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using DevLib.Application.CQRS.Dtos.Commands;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.Options;
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.CQRS.Commands.Auth.Login;

public class LoginWithSocialProviderCommandHandler(
    IAuthRepository authRepository, 
    UserManager<User> userManager, 
    IOptions<AuthenticationOptions> authenticationOptions): IRequestHandler<LoginWithSocialProviderCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginWithSocialProviderCommand request, CancellationToken cancellationToken)
    {
        var user = await authRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return CreateLoginResult(false, "User not found");
        }

        var userLogins = await authRepository.GetLoginsAsync(user, cancellationToken);

        if (userLogins.Any(l => l.LoginProvider == request.Provider && l.ProviderKey == request.UserId))
        {
            await userManager.SetAuthenticationTokenAsync(user, request.Provider, authenticationOptions.Value.Google.TokenName, request.IdToken);

            return CreateLoginResult(true, token: request.IdToken);
        }

        var loginInfo = new UserLoginInfo(request.Provider, request.UserId, request.Provider);

        var addLog = await authRepository.AddLoginAsync(user, loginInfo, cancellationToken);

        if (!addLog.Succeeded)
        {
            return CreateLoginResult(false, "Login failed");
        }

        var tokenResult = await userManager.SetAuthenticationTokenAsync(user, request.Provider, authenticationOptions.Value.Google.TokenName, request.IdToken);

        if (!tokenResult.Succeeded)
        {
            return CreateLoginResult(false, "Could not save token");
        }

        return CreateLoginResult(true);
    }

    private AuthResponseDto CreateLoginResult(bool success, string errorMessage = null, string token = null)
    {
        return new AuthResponseDto { IsSuccess = success, ErrorMessage = errorMessage, Token = token };
    }
}
