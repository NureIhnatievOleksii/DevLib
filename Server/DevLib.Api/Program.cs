using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DevLib.Api;
using DevLib.Infrastructure;
using DevLib.Infrastructure.Database;
using DevLib.Infrastructure.PreloadingInformation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  
              .AllowAnyMethod()                     
              .AllowAnyHeader()                      
              .AllowCredentials();                   
    });
});

builder.Services
    .ConfigureWebApiServices()
    .ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddDbContext<DevLibContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<SeedDataService>();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<TokenValidationMiddleware>(); 
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DevLibContext>();
    context.Database.Migrate();

    var seedService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
    seedService.SeedData();
}

app.UseRouting();

app.ConfigureWebApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();