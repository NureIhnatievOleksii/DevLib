using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.UserAggregate;
using DevLib.Infrastructure.Database;

namespace DevLib.Infrastructure.Repositories;

public class AuthRepository(UserManager<User> userManager, DevLibContext context) : IAuthRepository
{
    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo, CancellationToken cancellationToken)
    {
        return await userManager.AddLoginAsync(user, loginInfo);
    }

    public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
    {
        return await userManager.GetLoginsAsync(user);
    }

    public async Task<IdentityResult> RemoveTokenAsync(string token, CancellationToken cancellationToken)
    {
        var userToken = await context.UserTokens.FirstOrDefaultAsync(z => z.Value == token, cancellationToken);

        if (userToken == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Token not found." });
        }

        context.UserTokens.Remove(userToken);

        await context.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }
}
