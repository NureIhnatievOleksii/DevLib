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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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
        .AddEntityFrameworkStores<DevLibContext>()
        .AddDefaultTokenProviders();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        })
        .AddCookie()
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
            googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        });


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Введите 'Bearer' [пробел] и ваш токен внизу для доступа к защищённым ресурсам.",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IValidator<Customer>, CustomerValidator>();

        services.AddHostedService<UserInitializerService>();

        return services
            .AddRepositories()
            .AddServices()
            .AddValidators()
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(ApplicationAssemblyConfigurator).Assembly));
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthRepository, AuthRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IRatingRepository, RatingRepository>()
            .AddScoped<ICommentRepository, CommentRepository>()
            .AddScoped<IDirectoryRepository, DirectoryRepository>()
            .AddScoped<IArticleRepository, ArticleRepository>()
            .AddScoped<IBookmarkRepository, BookmarkRepository>()
            .AddScoped<INoteRepository, NoteRepository>()
            .AddScoped<IArticleRepository, ArticleRepository>()
            .AddScoped<ITagRepository, TagRepository>()
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IPostRepository, PostRepository>()
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IUserRepository, UserRepository>();
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
