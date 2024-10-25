using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using DevLib.Application.CQRS.Dtos.Commands;
using DevLib.Application.Interfaces.Services;
using DevLib.Application.Options;
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.CQRS.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IJwtService jwtService,
        IOptions<AuthenticationOptions> authenticationOptions) : IRequestHandler<UpdateUserCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(command.UserId.ToString())
                       ?? throw new Exception("User not found");

            user.Email = command.Email;
            user.UserName = command.UserName;
            user.Photo = command.Photo;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Failed to update user information." };
            }

            var token = await jwtService.GenerateJwtTokenAsync(user);

            var tokenResult = await userManager.SetAuthenticationTokenAsync(user, authenticationOptions.Value.DevLib.Provider, authenticationOptions.Value.DevLib.TokenName, token);
            if (!tokenResult.Succeeded)
            {
                return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Failed to save token." };
            }

            return new AuthResponseDto { IsSuccess = true, Token = token };
        }
    }
}
