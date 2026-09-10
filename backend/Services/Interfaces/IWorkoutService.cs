using WorkoutTracker.DTOs.Workout;

namespace WorkoutTracker.Services.Interfaces;

public interface IWorkoutService
{
    Task<IEnumerable<WorkoutResponse>> GetMyWorkoutsAsync(Guid userId);

    Task<WorkoutResponse?> GetByIdAsync(Guid userId, Guid workoutId);

    Task<WorkoutResponse> CreateAsync(
        Guid userId, 
        CreateWorkoutRequest request);

    Task<bool> UpdateAsync(
        Guid userId,
        Guid workoutId,
        UpdateWorkoutRequest request);

    Task<bool> DeleteAsync(
        Guid userId,
        Guid workoutId);    

    Task<IEnumerable<WorkoutProgressResponse>> GetProgressAsync(
        Guid userId,
        int year,
        int month);
}

//Service gets UserId from the controller, the controller will get it from the JWT token.
