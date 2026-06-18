using TimeTrackingApp.Application.Abstractions.Persistence;
using TimeTrackingApp.Application.Abstractions.Services;
using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Tracking;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.Enums;

namespace TimeTrackingApp.Application.Services;

public class ActivityTrackingService : IActivityTrackingService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityTrackingService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<Result<ActivityRecord>> TrackAsync(TrackActivityRequest request, CancellationToken cancellationToken = default)
    {
        var validation = ValidateRequest(request);
        if (!validation.IsSuccess)
        {
            return Result<ActivityRecord>.Failure(validation.Message);
        }

        var durationMinutes = Math.Round((request.EndedAtUtc - request.StartedAtUtc).TotalMinutes, 2);

        var record = new ActivityRecord
        {
            UserId = request.UserId,
            Category = request.Category,
            StartedAtUtc = request.StartedAtUtc,
            EndedAtUtc = request.EndedAtUtc,
            DurationMinutes = durationMinutes,
            Pages = request.Pages,
            ReadingType = request.ReadingType,
            ExerciseType = request.ExerciseType,
            WorkLocation = request.WorkLocation,
            HobbyName = request.HobbyName?.Trim()
        };

        await _activityRepository.AddAsync(record, cancellationToken);
        return Result<ActivityRecord>.Success(record, "Activity saved successfully.");
    }

    private static Result ValidateRequest(TrackActivityRequest request)
    {
        if (request.UserId == Guid.Empty)
        {
            return Result.Failure("A valid user is required.");
        }

        if (request.EndedAtUtc < request.StartedAtUtc)
        {
            return Result.Failure("End time cannot be earlier than start time.");
        }

        return request.Category switch
        {
            ActivityCategory.Reading when request.Pages is null || request.Pages <= 0 => Result.Failure("Pages must be greater than 0."),
            ActivityCategory.Reading when request.ReadingType is null => Result.Failure("Reading type is required."),
            ActivityCategory.Exercising when request.ExerciseType is null => Result.Failure("Exercise type is required."),
            ActivityCategory.Working when request.WorkLocation is null => Result.Failure("Work location is required."),
            ActivityCategory.OtherHobbies when string.IsNullOrWhiteSpace(request.HobbyName) => Result.Failure("Hobby name is required."),
            _ => Result.Success("Tracking request is valid.")
        };
    }
}
