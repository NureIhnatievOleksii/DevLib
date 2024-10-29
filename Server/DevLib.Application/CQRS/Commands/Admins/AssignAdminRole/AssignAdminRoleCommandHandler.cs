using DevLib.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Admins.AssignAdminRole
{
    public class AssignAdminRoleCommandHandler : IRequestHandler<AssignAdminRoleCommand, CommandResultDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AssignAdminRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<CommandResultDto> Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new CommandResultDto(false, "User not found.");
            }

            const string roleName = "Admin";

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return new CommandResultDto(false, "Admin role does not exist.");
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                return new CommandResultDto(true); 
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded
                ? new CommandResultDto(true)
                : new CommandResultDto(false, "Failed to assign admin role.");
        }
    }
}
