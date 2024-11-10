using Microsoft.AspNetCore.Http;
using System.Net;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value.ToLower();

        if (path.StartsWith("/swagger") ||
            path.StartsWith("/api/docs") ||
            path.StartsWith("/api/auth/register") ||
            path.StartsWith("/api/auth/login") ||
            path.StartsWith("/api/auth/login-with-social-provider") ||
            path.StartsWith("/api/auth/logout") ||
            path.StartsWith("/api/book/get-book/") ||
            path.StartsWith("/api/book/search-books") ||
            path.StartsWith("/api/book/last-published-books") ||
            path.StartsWith("/api/directory/get-article/") ||
            path.StartsWith("/api/directory/get-all-chapter-name/") ||
            path.StartsWith("/api/directory/get-directory/") ||
            path.StartsWith("/api/tag/get-tags/") ||
            path.StartsWith("/api/tag/get-tags") ||
            (path.StartsWith("/api/tag/") && path.EndsWith("/books")) ||
            path.StartsWith("/api/directory/search-directories/") ||
            path.StartsWith("/images") ||
            path.StartsWith("/books")) // Разрешение доступа ко всем файлам в папке Books
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403 Forbidden
            await context.Response.WriteAsync("Access denied. No token provided.");
            return;
        }

        await _next(context);
    }

}
