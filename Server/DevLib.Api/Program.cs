using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DevLib.Api;
using DevLib.Infrastructure;
using DevLib.Infrastructure.Database;
using DevLib.Infrastructure.PreloadingInformation;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureWebApiServices()
    .ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddDbContext<DevLibContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация SeedDataService
builder.Services.AddTransient<SeedDataService>();

var app = builder.Build();

app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DevLibContext>();
    context.Database.Migrate();

    var seedService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
    seedService.SeedData();
}

app.ConfigureWebApi();

app.Run();
var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();
