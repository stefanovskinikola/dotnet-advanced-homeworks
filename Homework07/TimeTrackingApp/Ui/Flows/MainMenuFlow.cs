using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Ui.Formatting;
using TimeTrackingApp.Ui.Navigation;

namespace TimeTrackingApp.Ui.Flows;

public class MainMenuFlow
{
    private readonly TrackingFlow _trackingFlow;
    private readonly StatisticsFlow _statisticsFlow;
    private readonly AccountManagementFlow _accountManagementFlow;

    public MainMenuFlow(
        TrackingFlow trackingFlow,
        StatisticsFlow statisticsFlow,
        AccountManagementFlow accountManagementFlow)
    {
        _trackingFlow = trackingFlow;
        _statisticsFlow = statisticsFlow;
        _accountManagementFlow = accountManagementFlow;
    }

    public async Task ShowAsync(User user)
    {
        while (true)
        {
            ConsoleStyler.WriteHeading($"Main Menu - {user.FirstName} {user.LastName}");
            var choice = InputHelper.ReadMenuChoice("Choose an action", new Dictionary<int, string>
            {
                [1] = "Track time",
                [2] = "User statistics",
                [3] = "Account management",
                [4] = "Log out"
            });

            switch (choice)
            {
                case 1:
                    await _trackingFlow.ShowAsync(user);
                    break;
                case 2:
                    await _statisticsFlow.ShowAsync(user);
                    break;
                case 3:
                    var shouldContinue = await _accountManagementFlow.ShowAsync(user);
                    if (!shouldContinue)
                    {
                        return;
                    }

                    break;
                case 4:
                    ConsoleStyler.WriteSuccess("Logged out successfully.");
                    ConsoleStyler.Pause();
                    return;
            }
        }
    }
}