namespace WorkoutTracker.DTOs.Workout;

public class WorkoutProgressResponse
{
    public int Week { get; set; }
    public int TotalDurationMinutes { get; set; }
    public int WorkoutCount { get; set; }
    public double AverageIntensityLevel { get; set; }
    public double AverageFatigueLevel { get; set; }
}
