using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Models.Tracking;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.Enums;
using TimeTrackingApp.Ui.Formatting;
using TimeTrackingApp.Ui.Navigation;

namespace TimeTrackingApp.Ui.Flows;

public class TrackingFlow
{
    private readonly IActivityTrackingService _activityTrackingService;

    public TrackingFlow(IActivityTrackingService activityTrackingService)
    {
        _activityTrackingService = activityTrackingService;
    }

    public async Task ShowAsync(User user)
    {
        while (true)
        {
            ConsoleStyler.WriteHeading("Track time");
            var choice = InputHelper.ReadMenuChoice("Activities", new Dictionary<int, string>
            {
                [1] = "Reading",
                [2] = "Exercising",
                [3] = "Working",
                [4] = "Other hobbies",
                [5] = "Back to Main Menu"
            });

            if (choice == 5)
            {
                return;
            }

            var category = (ActivityCategory)choice;
            var startedAtUtc = DateTime.UtcNow;
            ConsoleStyler.WriteInfo($"Tracking started for {category}. Press Enter to stop the timer.");
            Console.ReadLine();
            var endedAtUtc = DateTime.UtcNow;

            var request = BuildTrackingRequest(user.Id, category, startedAtUtc, endedAtUtc);
            var result = await _activityTrackingService.TrackAsync(request);
            if (!result.IsSuccess || result.Data is null)
            {
                ConsoleStyler.WriteError(result.Message);
                ConsoleStyler.Pause();
                continue;
            }

            ConsoleStyler.WriteSuccess($"You spent {result.Data.DurationMinutes:F2} minutes on {category}.");
            ConsoleStyler.Pause();
            return;
        }
    }

    private static TrackActivityRequest BuildTrackingRequest(Guid userId, ActivityCategory category, DateTime startedAtUtc, DateTime endedAtUtc)
    {
        var request = new TrackActivityRequest
        {
            UserId = userId,
            Category = category,
            StartedAtUtc = startedAtUtc,
            EndedAtUtc = endedAtUtc
        };

        switch (category)
        {
            case ActivityCategory.Reading:
                request.Pages = InputHelper.ReadIntInRange("Pages read: ", 1, int.MaxValue);
                request.ReadingType = (ReadingType)InputHelper.ReadMenuChoice("Reading type", new Dictionary<int, string>
                {
                    [1] = "Belles Lettres",
                    [2] = "Fiction",
                    [3] = "Professional Literature"
                });
                break;
            case ActivityCategory.Exercising:
                request.ExerciseType = (ExerciseType)InputHelper.ReadMenuChoice("Exercise type", new Dictionary<int, string>
                {
                    [1] = "General",
                    [2] = "Running",
                    [3] = "Sport"
                });
                break;
            case ActivityCategory.Working:
                request.WorkLocation = (WorkLocation)InputHelper.ReadMenuChoice("Work location", new Dictionary<int, string>
                {
                    [1] = "At the office",
                    [2] = "From home"
                });
                break;
            case ActivityCategory.OtherHobbies:
                request.HobbyName = InputHelper.ReadRequiredString("Hobby name: ");
                break;
        }

        return request;
    }
}