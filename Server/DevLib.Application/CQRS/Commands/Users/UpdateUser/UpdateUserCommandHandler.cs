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

            if (command.Photo != null)
            {
                var allowedImageExtensions = new[] { ".jpg", ".png", ".jpeg" };
                var imgExtension = Path.GetExtension(command.Photo.FileName).ToLower();

                if (!allowedImageExtensions.Contains(imgExtension))
                {
                    return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Invalid file extension. Allowed extensions are: .jpg, .png, .jpeg" };
                }

                string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserPhotos");
                if (!Directory.Exists(webRootPath))
                {
                    Directory.CreateDirectory(webRootPath);
                }

                var imageFileName = $"{DateTime.UtcNow.Ticks}_{Path.GetFileName(command.Photo.FileName)}";
                var imageDestinationPath = Path.Combine(webRootPath, imageFileName);

                try
                {
                    using (var stream = new FileStream(imageDestinationPath, FileMode.Create))
                    {
                        await command.Photo.CopyToAsync(stream, cancellationToken);
                    }

                    user.Photo = $"/UserPhotos/{imageFileName}";
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Photo file could not be copied" };
                }
            }

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
