using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Account;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.Abstractions.Services;

public interface IAccountService
{
    Task<Result> ChangePasswordAsync(User user, ChangePasswordRequest request, CancellationToken cancellationToken = default);
    Task<Result> ChangeFirstNameAsync(User user, string firstName, CancellationToken cancellationToken = default);
    Task<Result> ChangeLastNameAsync(User user, string lastName, CancellationToken cancellationToken = default);
    Task<Result> DeactivateAsync(User user, CancellationToken cancellationToken = default);
}
