namespace Catalog.Service.Common;

public enum ErrorType
{
    NotFound
}

public sealed record Error(ErrorType Type, string Message)
{
    public static Error NotFound(string message) => new(ErrorType.NotFound, message);
}
