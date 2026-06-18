using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.Models.Statistics;
using TimeTrackingApp.Ui.Formatting;
using TimeTrackingApp.Ui.Navigation;

namespace TimeTrackingApp.Ui.Flows;

public class StatisticsFlow
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsFlow(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task ShowAsync(User user)
    {
        while (true)
        {
            var statistics = await _statisticsService.GetByUserIdAsync(user.Id);
            ConsoleStyler.WriteHeading("User statistics");
            var choice = InputHelper.ReadMenuChoice("Statistics", new Dictionary<int, string>
            {
                [1] = "Reading",
                [2] = "Exercising",
                [3] = "Working",
                [4] = "Hobbies",
                [5] = "Global",
                [6] = "Back to Main Menu"
            });

            if (choice == 6)
            {
                return;
            }

            Console.WriteLine();
            switch (choice)
            {
                case 1:
                    PrintReadingStatistics(statistics.Reading);
                    break;
                case 2:
                    PrintExerciseStatistics(statistics.Exercising);
                    break;
                case 3:
                    PrintWorkStatistics(statistics.Working);
                    break;
                case 4:
                    PrintHobbyStatistics(statistics.Hobbies);
                    break;
                case 5:
                    PrintGlobalStatistics(statistics.Global);
                    break;
            }

            ConsoleStyler.Pause();
        }
    }

    private static void PrintReadingStatistics(ReadingStatistics statistics)
    {
        Console.WriteLine($"Total time: {statistics.TotalHours:F2} hours");
        Console.WriteLine($"Average record: {statistics.AverageMinutes:F2} minutes");
        Console.WriteLine($"Total pages: {statistics.TotalPages}");
        Console.WriteLine($"Favorite type: {statistics.FavoriteType}");
    }

    private static void PrintExerciseStatistics(ExerciseStatistics statistics)
    {
        Console.WriteLine($"Total time: {statistics.TotalHours:F2} hours");
        Console.WriteLine($"Average record: {statistics.AverageMinutes:F2} minutes");
        Console.WriteLine($"Favorite type: {statistics.FavoriteType}");
    }

    private static void PrintWorkStatistics(WorkStatistics statistics)
    {
        Console.WriteLine($"Total time: {statistics.TotalHours:F2} hours");
        Console.WriteLine($"Average record: {statistics.AverageMinutes:F2} minutes");
        Console.WriteLine($"Office work: {statistics.OfficeHours:F2} hours");
        Console.WriteLine($"Home work: {statistics.HomeHours:F2} hours");
    }

    private static void PrintHobbyStatistics(HobbyStatistics statistics)
    {
        Console.WriteLine($"Total time: {statistics.TotalHours:F2} hours");
        Console.WriteLine("Recorded hobbies:");

        if (statistics.HobbyNames.Count == 0)
        {
            Console.WriteLine("- None");
            return;
        }

        foreach (var hobbyName in statistics.HobbyNames)
        {
            Console.WriteLine($"- {hobbyName}");
        }
    }

    private static void PrintGlobalStatistics(GlobalStatistics statistics)
    {
        Console.WriteLine($"Total time: {statistics.TotalHours:F2} hours");
        Console.WriteLine($"Favorite activity: {statistics.FavoriteActivity}");
    }
}