using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories.Interfaces;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId);
    Task<Workout?> GetByIdAsync(Guid id);   
    Task AddAsync(Workout workout);
    Task UpdateAsync(Workout workout);
    Task DeleteAsync(Guid id);
}
// Repository must know how to get workouts by user id, get a workout by id, add a workout, update a workout, and delete a workout.
// But it does not tell how to do it. That is the job of the concrete implementation of the repository. The interface just defines the contract that the concrete implementation must follow.
// Interface because of separation of concerns and SOLID principles.