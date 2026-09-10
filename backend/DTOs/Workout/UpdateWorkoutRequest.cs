using WorkoutTracker.Entities;

namespace WorkoutTracker.DTOs.Workout;

public class UpdateWorkoutRequest
{
    public WorkoutType Type { get; set; }
    public DateTime DateTime { get; set; }
    public int DurationMinutes { get; set; } // Duration in minutes
    public int CaloriesBurned { get; set; } // Calories burned during the workout
    public int IntensityLevel { get; set; } // Intensity level of the workout (e.g., 1-10)
    public int FatigueLevel { get; set; } // Fatigue level after the workout (e.g., 1-10)
    public string? Notes { get; set; } // Optional notes about the workout
}
