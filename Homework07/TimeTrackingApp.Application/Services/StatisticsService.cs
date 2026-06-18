using TimeTrackingApp.Application.Abstractions.Persistence;
using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.Enums;
using TimeTrackingApp.Domain.Models.Statistics;

namespace TimeTrackingApp.Application.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IActivityRepository _activityRepository;

    public StatisticsService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<UserStatistics> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var records = await _activityRepository.GetByUserIdAsync(userId, cancellationToken);

        var reading = records.Where(x => x.Category == ActivityCategory.Reading).ToList();
        var exercising = records.Where(x => x.Category == ActivityCategory.Exercising).ToList();
        var working = records.Where(x => x.Category == ActivityCategory.Working).ToList();
        var hobbies = records.Where(x => x.Category == ActivityCategory.OtherHobbies).ToList();

        return new UserStatistics
        {
            Reading = new ReadingStatistics
            {
                TotalHours = ToHours(reading),
                AverageMinutes = ToAverageMinutes(reading),
                TotalPages = reading.Sum(x => x.Pages ?? 0),
                FavoriteType = GetFavoriteName(reading, x => x.ReadingType?.ToString())
            },
            Exercising = new ExerciseStatistics
            {
                TotalHours = ToHours(exercising),
                AverageMinutes = ToAverageMinutes(exercising),
                FavoriteType = GetFavoriteName(exercising, x => x.ExerciseType?.ToString())
            },
            Working = new WorkStatistics
            {
                TotalHours = ToHours(working),
                AverageMinutes = ToAverageMinutes(working),
                OfficeHours = Math.Round(working.Where(x => x.WorkLocation == WorkLocation.Office).Sum(x => x.DurationMinutes) / 60, 2),
                HomeHours = Math.Round(working.Where(x => x.WorkLocation == WorkLocation.Home).Sum(x => x.DurationMinutes) / 60, 2)
            },
            Hobbies = new HobbyStatistics
            {
                TotalHours = ToHours(hobbies),
                HobbyNames = hobbies
                    .Select(x => x.HobbyName?.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(x => x)
                    .Cast<string>()
                    .ToArray()
            },
            Global = new GlobalStatistics
            {
                TotalHours = ToHours(records),
                FavoriteActivity = GetFavoriteName(records, x => x.Category.ToString())
            }
        };
    }

    private static double ToHours(IEnumerable<ActivityRecord> records)
        => Math.Round(records.Sum(x => x.DurationMinutes) / 60, 2);

    private static double ToAverageMinutes(IReadOnlyCollection<ActivityRecord> records)
        => records.Count == 0 ? 0 : Math.Round(records.Average(x => x.DurationMinutes), 2);

    private static string GetFavoriteName(IEnumerable<ActivityRecord> records, Func<ActivityRecord, string?> selector)
    {
        return records
            .Select(selector)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .GroupBy(x => x!, StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(x => x.Count())
            .ThenBy(x => x.Key)
            .Select(x => x.Key)
            .FirstOrDefault() ?? "N/A";
    }
}
