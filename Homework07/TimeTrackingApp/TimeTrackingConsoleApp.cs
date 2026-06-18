using TimeTrackingApp.Ui.Flows;

namespace TimeTrackingApp;

public class TimeTrackingConsoleApp
{
    private readonly AuthenticationFlow _authenticationFlow;
    private readonly MainMenuFlow _mainMenuFlow;

    public TimeTrackingConsoleApp(AuthenticationFlow authenticationFlow, MainMenuFlow mainMenuFlow)
    {
        _authenticationFlow = authenticationFlow;
        _mainMenuFlow = mainMenuFlow;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            var loggedInUser = await _authenticationFlow.ShowAsync();
            if (loggedInUser is null)
            {
                return;
            }

            await _mainMenuFlow.ShowAsync(loggedInUser);
        }
    }
}
