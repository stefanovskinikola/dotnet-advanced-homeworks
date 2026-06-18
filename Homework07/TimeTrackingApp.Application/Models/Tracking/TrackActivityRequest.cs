using TimeTrackingApp.Domain.Enums;

namespace TimeTrackingApp.Application.Models.Tracking;

public class TrackActivityRequest
{
    public Guid UserId { get; set; }
    public ActivityCategory Category { get; set; }
    public DateTime StartedAtUtc { get; set; }
    public DateTime EndedAtUtc { get; set; }
    public int? Pages { get; set; }
    public ReadingType? ReadingType { get; set; }
    public ExerciseType? ExerciseType { get; set; }
    public WorkLocation? WorkLocation { get; set; }
    public string? HobbyName { get; set; }
}
