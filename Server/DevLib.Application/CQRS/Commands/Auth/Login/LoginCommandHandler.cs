using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using DevLib.Application.CQRS.Dtos.Commands;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.Interfaces.Services;
using DevLib.Application.Options;
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.CQRS.Commands.Auth.Login;

public class LoginCommandHandler(
    IAuthRepository authRepository,
    UserManager<User> userManager,
    IOptions<AuthenticationOptions> authenticationOptions,
    IJwtService jwtService) : IRequestHandler<LoginCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await authRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return CreateLoginResult(false, "User not found");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            return CreateLoginResult(false, "Invalid password");
        }

        // Получение роли пользователя
        var roles = await userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault(); // Предположим, что пользователь может иметь одну роль

        // Генерация токена с дополнительными параметрами
        var token = await jwtService.GenerateJwtTokenAsync(user);

        var tokenResult = await userManager.SetAuthenticationTokenAsync(user, authenticationOptions.Value.DevLib.Provider, authenticationOptions.Value.DevLib.TokenName, token);

        if (!tokenResult.Succeeded)
        {
            return CreateLoginResult(false, "Could not save token");
        }

        return CreateLoginResult(true, token: token);
    }

    private AuthResponseDto CreateLoginResult(bool success, string errorMessage = null, string token = null)
    {
        return new AuthResponseDto { IsSuccess = success, ErrorMessage = errorMessage, Token = token };
    }
}
