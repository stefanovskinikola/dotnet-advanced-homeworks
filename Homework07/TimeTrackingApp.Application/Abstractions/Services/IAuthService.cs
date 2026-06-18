using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Authentication;

namespace TimeTrackingApp.Application.Abstractions.Services;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
    Task<LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result> ReactivateAsync(Guid userId, CancellationToken cancellationToken = default);
}
