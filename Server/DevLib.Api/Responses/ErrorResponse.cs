namespace DevLib.Api.Responses;

public record ErrorResponse(string Message, string[] Details)
{
    public string Message { get; private set; } = Message;
    public string[] Details { get; private set; } = Details;
    public string StackTrace { get; private set; }

    public ErrorResponse WithErrorStackTrace(string stackTrace)
    {
        return this with { StackTrace = stackTrace };
    }
}