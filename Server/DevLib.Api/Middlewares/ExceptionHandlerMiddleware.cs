using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using DevLib.Domain.Exceptions;

namespace DevLib.Api.Middlewares;

public sealed class ExceptionHandlerMiddleware
    (IHostEnvironment environment, ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";
    private const string ErrorCodeKey = "ErrorCode";

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        AddErrorCode(exception);
        logger.LogError(exception, exception is ExceptionBase ? exception.Message : UnhandledExceptionMsg);

        var problemDetails = CreateProblemDetails(context, exception);
        var json = ToJson(problemDetails);

        const string contentType = "application/problem+json";
        context.Response.ContentType = contentType;
        await context.Response.WriteAsync(json);

        context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
    }

    private void AddErrorCode(Exception exception, string errorCode = "UNKNOWN_ERROR")
    {
        if (exception.Data.Contains(ErrorCodeKey))
        {
            exception.Data[ErrorCodeKey] = errorCode;
        }
        else
        {
            exception.Data.Add(ErrorCodeKey, errorCode);
        }
    }

    private string GetErrorCode(Exception exception)
    {
        return exception.Data.Contains(ErrorCodeKey) ? exception.Data[ErrorCodeKey].ToString() : "UNKNOWN_ERROR";
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var errorCode = GetErrorCode(exception);
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(context.Response.StatusCode);

        if (string.IsNullOrEmpty(reasonPhrase))
        {
            reasonPhrase = UnhandledExceptionMsg;
        }

        var problemDetails = new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Title = reasonPhrase,
            Extensions =
            {
                [nameof(errorCode)] = errorCode
            }
        };

        if (!environment.IsDevelopment())
        {
            return problemDetails;
        }

        problemDetails.Detail = exception.ToString();
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;
        problemDetails.Extensions["data"] = exception.Data;

        return problemDetails;
    }

    private string ToJson(ProblemDetails problemDetails)
    {
        try
        {
            return JsonSerializer.Serialize(problemDetails, JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            const string msg = "An exception has occurred while serializing error to JSON";
            logger.LogError(ex, msg);
        }

        return string.Empty;
    }
}
