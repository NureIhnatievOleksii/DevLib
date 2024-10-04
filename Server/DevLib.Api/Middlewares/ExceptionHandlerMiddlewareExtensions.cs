namespace DevLib.Api.Middlewares;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IServiceCollection AddExceptionHandlerMiddleware(this IServiceCollection services)
    {
        return services.AddSingleton<ExceptionHandlerMiddleware>();
    }

    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
