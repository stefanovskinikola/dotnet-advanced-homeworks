namespace TimeTrackingApp.Application.Models;

public class Result
{
    protected Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    public string Message { get; }

    public static Result Success(string message) => new(true, message);
    public static Result Failure(string message) => new(false, message);
}

public class Result<T> : Result
{
    private Result(bool isSuccess, string message, T? data)
        : base(isSuccess, message)
    {
        Data = data;
    }

    public T? Data { get; }

    public static Result<T> Success(T data, string message) => new(true, message, data);
    public new static Result<T> Failure(string message) => new(false, message, default);
}
