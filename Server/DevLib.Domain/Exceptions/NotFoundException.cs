using System.Net;

namespace DevLib.Domain.Exceptions;

public class NotFoundException(string message, params string[] errorDetails) : ExceptionBase(message, errorDetails)
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.NotFound;
}