using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Models.Account;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Ui.Formatting;
using TimeTrackingApp.Ui.Navigation;

namespace TimeTrackingApp.Ui.Flows;

public class AccountManagementFlow
{
    private readonly IAccountService _accountService;

    public AccountManagementFlow(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<bool> ShowAsync(User user)
    {
        while (true)
        {
            ConsoleStyler.WriteHeading("Account management");
            var choice = InputHelper.ReadMenuChoice("Account options", new Dictionary<int, string>
            {
                [1] = "Change password",
                [2] = "Change First Name",
                [3] = "Change Last Name",
                [4] = "Deactivate account",
                [5] = "Back to Main Menu"
            });

            switch (choice)
            {
                case 1:
                    await ChangePasswordAsync(user);
                    break;
                case 2:
                    await ChangeFirstNameAsync(user);
                    break;
                case 3:
                    await ChangeLastNameAsync(user);
                    break;
                case 4:
                    if (await DeactivateAccountAsync(user))
                    {
                        return false;
                    }

                    break;
                case 5:
                    return true;
            }
        }
    }

    private async Task ChangePasswordAsync(User user)
    {
        ConsoleStyler.WriteHeading("Change password");
        var result = await _accountService.ChangePasswordAsync(user, new ChangePasswordRequest
        {
            CurrentPassword = InputHelper.ReadRequiredString("Current password: "),
            NewPassword = InputHelper.ReadRequiredString("New password: ")
        });

        ResultWriter.Write(result.IsSuccess, result.Message);
        ConsoleStyler.Pause();
    }

    private async Task ChangeFirstNameAsync(User user)
    {
        ConsoleStyler.WriteHeading("Change First Name");
        var result = await _accountService.ChangeFirstNameAsync(user, InputHelper.ReadRequiredString("New first name: "));
        ResultWriter.Write(result.IsSuccess, result.Message);
        ConsoleStyler.Pause();
    }

    private async Task ChangeLastNameAsync(User user)
    {
        ConsoleStyler.WriteHeading("Change Last Name");
        var result = await _accountService.ChangeLastNameAsync(user, InputHelper.ReadRequiredString("New last name: "));
        ResultWriter.Write(result.IsSuccess, result.Message);
        ConsoleStyler.Pause();
    }

    private async Task<bool> DeactivateAccountAsync(User user)
    {
        ConsoleStyler.WriteHeading("Deactivate account");
        if (!InputHelper.ReadYesNo("Are you sure you want to deactivate your account?"))
        {
            return false;
        }

        var result = await _accountService.DeactivateAsync(user);
        ResultWriter.Write(result.IsSuccess, result.Message);
        ConsoleStyler.Pause();
        return result.IsSuccess;
    }
}