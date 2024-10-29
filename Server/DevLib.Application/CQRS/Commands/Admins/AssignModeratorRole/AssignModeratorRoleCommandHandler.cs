using DevLib.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Admins.AssignModeratorRole
{
    public class AssignModeratorRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager
) : IRequestHandler<AssignModeratorRoleCommand, CommandResultDto>
    {
        public async Task<CommandResultDto> Handle(AssignModeratorRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new CommandResultDto(false, "User not found.");
            }

            const string roleName = "Moderator";

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }

            var currentRoles = await userManager.GetRolesAsync(user);
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return new CommandResultDto(false, "Failed to remove existing roles.");
            }

            var result = await userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded
                ? new CommandResultDto(true)
                : new CommandResultDto(false, "Failed to assign moderator role.");
        }
    }


}
