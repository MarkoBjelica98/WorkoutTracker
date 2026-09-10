using WorkoutTracker.Entities;

namespace WorkoutTracker.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
// Service will get User and return a JWT token.