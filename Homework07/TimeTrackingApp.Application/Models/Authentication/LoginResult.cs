using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.Models.Authentication;

public class LoginResult
{
    public bool IsSuccess { get; init; }
    public bool RequiresReactivation { get; init; }
    public string Message { get; init; } = string.Empty;
    public User? User { get; init; }

    public static LoginResult Success(User user, string message) => new()
    {
        IsSuccess = true,
        Message = message,
        User = user
    };

    public static LoginResult Failure(string message) => new()
    {
        IsSuccess = false,
        Message = message
    };

    public static LoginResult ReactivationRequired(User user, string message) => new()
    {
        IsSuccess = false,
        RequiresReactivation = true,
        Message = message,
        User = user
    };
}
