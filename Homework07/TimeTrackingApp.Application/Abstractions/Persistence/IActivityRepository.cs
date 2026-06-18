using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.Abstractions.Persistence;

public interface IActivityRepository
{
    Task<IReadOnlyCollection<ActivityRecord>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(ActivityRecord activityRecord, CancellationToken cancellationToken = default);
}
