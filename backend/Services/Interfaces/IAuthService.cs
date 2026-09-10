using Microsoft.AspNetCore.Authentication.OAuth;
using WorkoutTracker.DTOs.Auth;

namespace WorkoutTracker.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);
}
