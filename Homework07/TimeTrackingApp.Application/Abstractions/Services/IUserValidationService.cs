using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Authentication;

namespace TimeTrackingApp.Application.Abstractions.Services;

public interface IUserValidationService
{
    Result ValidateRegistration(RegisterUserRequest request);
    Result ValidateAge(int age);
    Result ValidateName(string value, string fieldName);
    Result ValidateUsername(string username);
    Result ValidatePassword(string password);
}
