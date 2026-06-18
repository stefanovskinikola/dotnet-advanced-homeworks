using TimeTrackingApp.Application.Abstractions.Persistence;
using TimeTrackingApp.Application.Abstractions.Security;
using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Account;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidationService _userValidationService;
    private readonly IPasswordHasher _passwordHasher;

    public AccountService(
        IUserRepository userRepository,
        IUserValidationService userValidationService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _userValidationService = userValidationService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> ChangePasswordAsync(User user, ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
        {
            return Result.Failure("Current password is incorrect.");
        }

        var validation = _userValidationService.ValidatePassword(request.NewPassword);
        if (!validation.IsSuccess)
        {
            return validation;
        }

        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result.Success("Password changed successfully.");
    }

    public async Task<Result> ChangeFirstNameAsync(User user, string firstName, CancellationToken cancellationToken = default)
    {
        var validation = _userValidationService.ValidateName(firstName, "First name");
        if (!validation.IsSuccess)
        {
            return validation;
        }

        user.FirstName = firstName.Trim();
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result.Success("First name changed successfully.");
    }

    public async Task<Result> ChangeLastNameAsync(User user, string lastName, CancellationToken cancellationToken = default)
    {
        var validation = _userValidationService.ValidateName(lastName, "Last name");
        if (!validation.IsSuccess)
        {
            return validation;
        }

        user.LastName = lastName.Trim();
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result.Success("Last name changed successfully.");
    }

    public async Task<Result> DeactivateAsync(User user, CancellationToken cancellationToken = default)
    {
        user.IsDeactivated = true;
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result.Success("Account deactivated successfully.");
    }
}
