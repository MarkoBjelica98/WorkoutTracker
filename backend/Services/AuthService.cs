using WorkoutTracker.DTOs.Auth;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Services.Interfaces;

namespace WorkoutTracker.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse?> RegisterAsync(
        RegisterRequest registerRequest)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerRequest.Email);

        if (existingUser != null)
        {
            return null;
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = registerRequest.Email,
        };

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

        await _userRepository.AddAsync(user);

        var token = _jwtService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);
        if (user == null)
        {
            return null;
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);

        if (!passwordValid)
        {
            return null;
        }

        var token = _jwtService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token
        };
    }
}
