using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.Interfaces.Services;
using DevLib.Application.Options;
using DevLib.Application.Services;
using DevLib.Application.Validation;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.UserAggregate;
using DevLib.Infrastructure.Database;
using DevLib.Infrastructure.Repositories;
using ApplicationAssemblyConfigurator = DevLib.Infrastructure.AssemblyConfigurator;

namespace DevLib.Infrastructure;

public static class AssemblyConfigurator
{
    private const string DevLibSqlServer = "DevLibSqlServer";

    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DevLibContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DevLibSqlServer)));

        services.AddAuthorization();

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        })
            .AddEntityFrameworkStores<DevLibContext>();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
            googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        });

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IValidator<Customer>, CustomerValidator>();

        return services
            .AddAutoMapper(typeof(MappingProfile))
            .AddServices()
            .AddRepositories()
            .AddValidators()
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(ApplicationAssemblyConfigurator).Assembly));
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthRepository, AuthRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IJwtService, JwtService>();
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services.AddScoped<IValidator<Customer>, CustomerValidator>();
    }
}
