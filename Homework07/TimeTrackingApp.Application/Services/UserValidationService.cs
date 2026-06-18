using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Authentication;

namespace TimeTrackingApp.Application.Services;

public partial class UserValidationService : IUserValidationService
{
    public Result ValidateRegistration(RegisterUserRequest request)
    {
        var firstNameValidation = ValidateName(request.FirstName, "First name");
        if (!firstNameValidation.IsSuccess)
        {
            return firstNameValidation;
        }

        var lastNameValidation = ValidateName(request.LastName, "Last name");
        if (!lastNameValidation.IsSuccess)
        {
            return lastNameValidation;
        }

        var ageValidation = ValidateAge(request.Age);
        if (!ageValidation.IsSuccess)
        {
            return ageValidation;
        }

        var usernameValidation = ValidateUsername(request.Username);
        if (!usernameValidation.IsSuccess)
        {
            return usernameValidation;
        }

        return ValidatePassword(request.Password);
    }

    public Result ValidateAge(int age)
    {
        if (age < 18 || age > 120)
        {
            return Result.Failure("Age must be between 18 and 120.");
        }

        return Result.Success("Age is valid.");
    }

    public Result ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 2)
        {
            return Result.Failure($"{fieldName} must be at least 2 characters long.");
        }

        if (value.Any(char.IsDigit))
        {
            return Result.Failure($"{fieldName} must not contain numbers.");
        }

        return Result.Success($"{fieldName} is valid.");
    }

    public Result ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Trim().Length < 5)
        {
            return Result.Failure("Username must be at least 5 characters long.");
        }

        return Result.Success("Username is valid.");
    }

    public Result ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            return Result.Failure("Password must be at least 6 characters long.");
        }

        if (!password.Any(char.IsUpper))
        {
            return Result.Failure("Password must contain at least one capital letter.");
        }

        if (!password.Any(char.IsDigit))
        {
            return Result.Failure("Password must contain at least one number.");
        }

        return Result.Success("Password is valid.");
    }
}
