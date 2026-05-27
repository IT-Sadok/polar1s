namespace Warranty.Service.Common;

public sealed class Result
{
    public bool IsSuccess { get; }
    public IReadOnlyCollection<Error> Errors { get; }

    private Result(bool isSuccess, IReadOnlyCollection<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success() => new(true, []);
    public static Result Failure(Error error) => new(false, [error]);
    public static Result Failure(IEnumerable<Error> errors) => new(false, [..errors]);

    public static implicit operator Result(Error error) => Failure(error);
}

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public IReadOnlyCollection<Error> Errors { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        Errors = [];
    }

    private Result(IReadOnlyCollection<Error> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new([error]);
    public static Result<T> Failure(IEnumerable<Error> errors) => new([..errors]);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}
