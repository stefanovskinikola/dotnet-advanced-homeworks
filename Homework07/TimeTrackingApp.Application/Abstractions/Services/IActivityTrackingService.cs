using TimeTrackingApp.Application.Models;
using TimeTrackingApp.Application.Models.Tracking;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.Abstractions.Services;

public interface IActivityTrackingService
{
    Task<Result<ActivityRecord>> TrackAsync(TrackActivityRequest request, CancellationToken cancellationToken = default);
}
