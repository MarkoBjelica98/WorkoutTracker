namespace WorkoutTracker.Entities;
    public class Workout
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // Foreign key to the User entity
        public User User { get; set; } = null!; // Navigation property to the User entity
        public WorkoutType Type { get; set; }
        public DateTime DateTime { get; set; }
        public int DurationMinutes { get; set; } // Duration in minutes
        public int CaloriesBurned { get; set; } // Calories burned during the workout
        public int IntensityLevel { get; set; } // Intensity level of the workout (e.g., 1-10)
        public int FatigueLevel { get; set; } // Fatigue level after the workout (e.g., 1-10)
        public string? Notes { get; set; } // Optional notes about the workout
}

