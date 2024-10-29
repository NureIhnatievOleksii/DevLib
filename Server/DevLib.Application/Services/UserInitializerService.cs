using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using DevLib.Application.CQRS.Commands.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.Services
{
    public class UserInitializerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserInitializerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                var registerCommand = new RegisterCommand("adminUser", "admin@nure.com", "DevLibAdmin123");

                var result = await mediator.Send(registerCommand, cancellationToken);

                if (result.IsSuccess)
                {
                    var user = await userManager.FindByEmailAsync(registerCommand.Email);

                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
                    }

                    if (await userManager.IsInRoleAsync(user, "Client"))
                    {
                        await userManager.RemoveFromRoleAsync(user, "Client");
                    }

                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    Console.WriteLine($"Failed to register admin user: {result.ErrorMessage}");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
