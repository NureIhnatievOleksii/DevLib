using DevLib.Api.Middlewares;
using DevLib.Application.CQRS.Queries.Customers.GetAllCustomers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevLib.Api;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
    {
        //services.AddExceptionHandlerMiddleware();
        
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            })
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)));

        // todo gi figure out how to avoid using GetAllCustomersQueryHandler
        // todo gi probably register services by assembly name
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllCustomersQueryHandler).Assembly));

        return services;
    }

    public static WebApplication ConfigureWebApi(this WebApplication app)
    {
        app.UseCors("AllowAll");
        //app.UseExceptionHandlerMiddleware();

        app.UseRouting();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        return app;
    }
}