namespace TimeTrackingApp.Domain.Models.Statistics;

public class ReadingStatistics
{
    public double TotalHours { get; set; }
    public double AverageMinutes { get; set; }
    public int TotalPages { get; set; }
    public string FavoriteType { get; set; } = "N/A";
}
