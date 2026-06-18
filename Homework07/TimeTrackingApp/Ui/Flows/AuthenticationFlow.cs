using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Authentication;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Ui.Formatting;
using TimeTrackingApp.Ui.Navigation;

namespace TimeTrackingApp.Ui.Flows;

public class AuthenticationFlow
{
    private readonly IAuthService _authService;
    private readonly IUserValidationService _userValidationService;

    public AuthenticationFlow(IAuthService authService, IUserValidationService userValidationService)
    {
        _authService = authService;
        _userValidationService = userValidationService;
    }

    public async Task<User?> ShowAsync()
    {
        var failedLoginAttempts = 0;

        while (true)
        {
            ConsoleStyler.WriteHeading("Time Tracking App");
            var choice = InputHelper.ReadMenuChoice("Authentication", new Dictionary<int, string>
            {
                [1] = "Log in",
                [2] = "Register",
                [3] = "Exit"
            });

            switch (choice)
            {
                case 1:
                    var loginResult = await HandleLoginAsync();
                    if (loginResult.User is not null && loginResult.IsSuccess)
                    {
                        return loginResult.User;
                    }

                    if (loginResult.User is not null && loginResult.RequiresReactivation)
                    {
                        var shouldReactivate = InputHelper.ReadYesNo(loginResult.Message);
                        if (shouldReactivate)
                        {
                            var result = await _authService.ReactivateAsync(loginResult.User.Id);
                            if (result.IsSuccess)
                            {
                                loginResult.User.IsDeactivated = false;
                                ConsoleStyler.WriteSuccess(result.Message);
                                ConsoleStyler.Pause();
                                return loginResult.User;
                            }

                            ConsoleStyler.WriteError(result.Message);
                            ConsoleStyler.Pause();
                        }

                        break;
                    }

                    failedLoginAttempts++;
                    ConsoleStyler.WriteError(loginResult.Message);

                    if (failedLoginAttempts >= 3)
                    {
                        ConsoleStyler.WriteInfo("Goodbye.");
                        return null;
                    }

                    ConsoleStyler.WriteInfo($"Remaining attempts: {3 - failedLoginAttempts}");
                    ConsoleStyler.Pause();
                    break;
                case 2:
                    await HandleRegisterAsync();
                    break;
                case 3:
                    ConsoleStyler.WriteInfo("Goodbye.");
                    return null;
            }
        }
    }

    private async Task<LoginResult> HandleLoginAsync()
    {
        ConsoleStyler.WriteHeading("Log in");
        var username = InputHelper.ReadRequiredString("Username: ");
        var password = InputHelper.ReadRequiredString("Password: ");
        return await _authService.LoginAsync(new LoginRequest
        {
            Username = username,
            Password = password
        });
    }

    private async Task HandleRegisterAsync()
    {
        ConsoleStyler.WriteHeading("Register");

        var request = new RegisterUserRequest
        {
            FirstName = ReadValidatedRegistrationField(
                "First name: ",
                value => _userValidationService.ValidateName(value, "First name")),
            LastName = ReadValidatedRegistrationField(
                "Last name: ",
                value => _userValidationService.ValidateName(value, "Last name")),
            Age = ReadValidatedAge(),
            Username = ReadValidatedRegistrationField(
                "Username: ",
                value => _userValidationService.ValidateUsername(value)),
            Password = ReadValidatedRegistrationField(
                "Password: ",
                value => _userValidationService.ValidatePassword(value),
                trimInput: false)
        };

        var result = await _authService.RegisterAsync(request);
        ResultWriter.Write(result.IsSuccess, result.Message);
        ConsoleStyler.Pause();
    }

    private string ReadValidatedRegistrationField(string prompt, Func<string, Result> validator, bool trimInput = true)
    {
        return InputHelper.ReadValidatedString(
            prompt,
            value => trimInput ? value.Trim() : value,
            value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return "Value is required.";
                }

                var validationResult = validator(value);
                return validationResult.IsSuccess ? null : validationResult.Message;
            });
    }

    private int ReadValidatedAge()
    {
        while (true)
        {
            var age = InputHelper.ReadIntInRange("Age: ", 0, 120);
            var validationResult = _userValidationService.ValidateAge(age);
            if (validationResult.IsSuccess)
            {
                return age;
            }

            ConsoleStyler.WriteError(validationResult.Message);
        }
    }
}