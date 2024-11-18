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
            path.StartsWith("/api/book/") ||
            path.StartsWith("/api/book/last-published-books") ||
            path.StartsWith("/api/directory/get-article/") ||
            path.StartsWith("/api/directory/get-last-directory/") ||
            path.StartsWith("/api/directory/get-all-chapter-name/") ||
            path.StartsWith("/api/directory/get-directory/") ||
            path.StartsWith("/api/directory/search-directory/") ||
            path.StartsWith("/api/tag/get-tags/") ||
            path.StartsWith("/api/tag/get-tags") ||
            (path.StartsWith("/api/tag/") && path.EndsWith("/books")) ||
            path.StartsWith("/api/directory/search-directories/") ||
            path.StartsWith("/api/user/forgot-password") ||
            path.StartsWith("/images") ||
            path.StartsWith("/books")) 
        {
            await _next(context);
            return;
        }

        if ((path.StartsWith("/api/post") || path.StartsWith("/api/post/")) && context.Request.Method == "GET")
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsync("Access denied. No token provided.");
            return;
        }

        await _next(context);
    }
}
