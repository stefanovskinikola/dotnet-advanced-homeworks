namespace TimeTrackingApp.Domain.Models.Statistics;

public class HobbyStatistics
{
    public double TotalHours { get; set; }
    public IReadOnlyCollection<string> HobbyNames { get; set; } = Array.Empty<string>();
}
