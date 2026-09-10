using WorkoutTracker.Entities; 

namespace WorkoutTracker.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id); // Find a user by their unique identifier asynchronously
    Task<User?> GetByEmailAsync(string email); // Find a user by their email address asynchronously
    Task AddAsync (User user); // Add a new user to the repository asynchronously
}
