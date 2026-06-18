namespace TimeTrackingApp.Domain.Models.Statistics;

public class UserStatistics
{
    public ReadingStatistics Reading { get; set; } = new();
    public ExerciseStatistics Exercising { get; set; } = new();
    public WorkStatistics Working { get; set; } = new();
    public HobbyStatistics Hobbies { get; set; } = new();
    public GlobalStatistics Global { get; set; } = new();
}
