using TimeTrackingApp.Domain.Models.Statistics;

namespace TimeTrackingApp.Application.Abstractions.Services;

public interface IStatisticsService
{
    Task<UserStatistics> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
