using DevLib.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DevLib.Api.Middlewares
{
    public class BannedUserMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                var user = await userManager.FindByIdAsync(userId.ToString());

                if (user?.IsBanned == true)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("User is banned.");
                    return;
                }
            }

            await next(context);
        }
    }

}
