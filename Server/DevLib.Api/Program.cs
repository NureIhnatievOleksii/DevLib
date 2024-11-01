using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DevLib.Api;
using DevLib.Infrastructure;
using DevLib.Infrastructure.Database;
using DevLib.Infrastructure.PreloadingInformation;

var builder = WebApplication.CreateBuilder(args);

// Настройка CORS для разрешения запросов с http://localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Укажите конкретный разрешенный домен
              .AllowAnyMethod()                     // Разрешаем все методы (GET, POST, PUT, DELETE и т.д.)
              .AllowAnyHeader()                     // Разрешаем все заголовки
              .AllowCredentials();                  // Разрешаем отправку куки и авторизационных данных
    });
});

// Регистрация сервисов
builder.Services
    .ConfigureWebApiServices()
    .ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddDbContext<DevLibContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация SeedDataService
builder.Services.AddTransient<SeedDataService>();

var app = builder.Build();
app.UseMiddleware<TokenValidationMiddleware>();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DevLibContext>();
    context.Database.Migrate();

    var seedService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
    seedService.SeedData();
}

// Применение CORS middleware до любого другого middleware
app.UseCors("AllowSpecificOrigin");

//app.UseMiddleware<TokenValidationMiddleware>(); // Проверка токенов после CORS
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DevLibContext>();
    context.Database.Migrate();

    var seedService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
    seedService.SeedData();
}

app.UseRouting();

// Подключаем другие middlewares и службы
app.ConfigureWebApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Валидация конфигурации AutoMapper
var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();