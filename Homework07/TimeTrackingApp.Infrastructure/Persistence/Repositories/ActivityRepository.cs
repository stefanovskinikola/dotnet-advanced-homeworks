using TimeTrackingApp.Application.Abstractions.Persistence;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Infrastructure.Persistence.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly JsonFileStore _fileStore;
    private readonly AppDataPaths _appDataPaths;

    public ActivityRepository(JsonFileStore fileStore, AppDataPaths appDataPaths)
    {
        _fileStore = fileStore;
        _appDataPaths = appDataPaths;
    }

    public async Task<IReadOnlyCollection<ActivityRecord>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var activities = await _fileStore.ReadCollectionAsync<ActivityRecord>(_appDataPaths.ActivitiesFilePath, cancellationToken);
        return activities.Where(x => x.UserId == userId).ToList();
    }

    public async Task AddAsync(ActivityRecord activityRecord, CancellationToken cancellationToken = default)
    {
        var activities = (await _fileStore.ReadCollectionAsync<ActivityRecord>(_appDataPaths.ActivitiesFilePath, cancellationToken)).ToList();
        activities.Add(activityRecord);
        await _fileStore.WriteCollectionAsync(_appDataPaths.ActivitiesFilePath, activities, cancellationToken);
    }
}
