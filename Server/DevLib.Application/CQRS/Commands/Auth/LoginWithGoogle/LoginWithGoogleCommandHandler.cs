using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using DevLib.Application.CQRS.Dtos.Commands;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.Options;
using DevLib.Domain.UserAggregate;
using System.IdentityModel.Tokens.Jwt;
using DevLib.Application.Interfaces.Services;

namespace DevLib.Application.CQRS.Commands.Auth.Login
{
    public class LoginWithGoogleCommandHandler(
        IAuthRepository authRepository,
        UserManager<User> userManager,
        IOptions<AuthenticationOptions> authenticationOptions,
        IJwtService jwtService) : IRequestHandler<LoginWithGoogleCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(request.Token);

            var email = jwtToken.Claims.First(claim => claim.Type == "email").Value;
            var provider = jwtToken.Claims.First(claim => claim.Type == "iss").Value;

            var user = await authRepository.FindByEmailAsync(email, cancellationToken);

            if (user == null)
            {
                return CreateLoginResult(false, "User not found");
            }

            var userLogins = await authRepository.GetLoginsAsync(user, cancellationToken);

            if (userLogins.Any(l => l.LoginProvider == provider && l.ProviderKey == request.UserId))
            {
                var token = await jwtService.GenerateJwtTokenAsync(user);

                await userManager.SetAuthenticationTokenAsync(user, provider, authenticationOptions.Value.Google.TokenName, request.Token);

                return CreateLoginResult(true, token: token);
            }

            var loginInfo = new UserLoginInfo(provider, request.UserId, provider);

            var addLog = await authRepository.AddLoginAsync(user, loginInfo, cancellationToken);

            if (!addLog.Succeeded)
            {
                return CreateLoginResult(false, "Login failed");
            }

            var tokenResult = await userManager.SetAuthenticationTokenAsync(user, provider, authenticationOptions.Value.Google.TokenName, request.Token);

            if (!tokenResult.Succeeded)
            {
                return CreateLoginResult(false, "Could not save token");
            }

            // Генерация токена после успешного входа
            var generatedToken = await jwtService.GenerateJwtTokenAsync(user);

            return CreateLoginResult(true, token: generatedToken);
        }

        private AuthResponseDto CreateLoginResult(bool success, string errorMessage = null, string token = null)
        {
            return new AuthResponseDto { IsSuccess = success, ErrorMessage = errorMessage, Token = token };
        }
    }
}
