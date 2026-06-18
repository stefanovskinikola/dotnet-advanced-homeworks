using TimeTrackingApp.Application.Abstractions.Persistence;
using TimeTrackingApp.Application.Abstractions.Security;
using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Authentication;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidationService _userValidationService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IUserValidationService userValidationService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _userValidationService = userValidationService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var validation = _userValidationService.ValidateRegistration(request);
        if (!validation.IsSuccess)
        {
            return validation;
        }

        var existingUser = await _userRepository.GetByUsernameAsync(request.Username.Trim(), cancellationToken);
        if (existingUser is not null)
        {
            return Result.Failure("Username is already taken.");
        }

        var user = new User
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Age = request.Age,
            Username = request.Username.Trim(),
            PasswordHash = _passwordHasher.Hash(request.Password)
        };

        await _userRepository.AddAsync(user, cancellationToken);
        return Result.Success("Registration completed successfully.");
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username.Trim(), cancellationToken);
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return LoginResult.Failure("Invalid username or password.");
        }

        if (user.IsDeactivated)
        {
            return LoginResult.ReactivationRequired(user, "Your account is deactivated. Do you want to activate it again?");
        }

        return LoginResult.Success(user, $"Welcome back, {user.FirstName}.");
    }

    public async Task<Result> ReactivateAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            return Result.Failure("User was not found.");
        }

        user.IsDeactivated = false;
        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result.Success("Your account has been activated.");
    }
}
