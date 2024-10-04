using System.Net;

namespace DevLib.Domain.Exceptions;

public class BadRequestException(string message, params string[] errorDetails) : ExceptionBase(message, errorDetails)
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.BadRequest;
}