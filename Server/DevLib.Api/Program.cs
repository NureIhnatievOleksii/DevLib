using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DevLib.Api;
using DevLib.Infrastructure;
using DevLib.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureWebApiServices()
    .ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddDbContext<DevLibContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.ConfigureWebApi();

app.Run();
var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();
