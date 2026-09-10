namespace WorkoutTracker.Entities;
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Workout> Workouts { get; set; } = new List<Workout>(); // One user can have more training sessions
}

