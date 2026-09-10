using WorkoutTracker.DTOs.Workout;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace WorkoutTracker.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;

    public WorkoutService(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<IEnumerable<WorkoutResponse>> GetMyWorkoutsAsync(Guid userId)
    {
        var workouts = await _workoutRepository.GetByUserIdAsync(userId);

        return workouts.Select(MapToResponse);
    }

    public async Task<WorkoutResponse?> GetByIdAsync(
        Guid userId, 
        Guid workoutId)

    {
        var workout = await _workoutRepository.GetByIdAsync(workoutId);

        if (workout == null || workout.UserId != userId)
        {
            return null;
        }

        return MapToResponse(workout);
    }

    public async Task<WorkoutResponse> CreateAsync(
        Guid userId,
        CreateWorkoutRequest request)
    {
        var workout = new Workout
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = request.Type,
            DateTime = request.DateTime,
            DurationMinutes = request.DurationMinutes,
            CaloriesBurned = request.CaloriesBurned,
            IntensityLevel = request.IntensityLevel,
            FatigueLevel = request.FatigueLevel,
            Notes = request.Notes
        };

        await _workoutRepository.AddAsync(workout);

        return MapToResponse(workout);
    }

    public async Task<bool> UpdateAsync(
        Guid userId,
        Guid workoutId,
        UpdateWorkoutRequest request)
    {
        var workout =
            await _workoutRepository.GetByIdAsync(workoutId);

        if (workout == null || workout.UserId != userId)
        {
            return false;
        }
        
        workout.Type = request.Type;
        workout.DateTime = request.DateTime;
        workout.DurationMinutes = request.DurationMinutes;
        workout.CaloriesBurned = request.CaloriesBurned;
        workout.IntensityLevel = request.IntensityLevel;    
        workout.FatigueLevel = request.FatigueLevel;
        workout.Notes = request.Notes;

        await _workoutRepository.UpdateAsync(workout);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid userId,
        Guid workoutId)
    {
        var workout =
            await _workoutRepository.GetByIdAsync(workoutId);

        if (workout == null || workout.UserId != userId)
        {
            return false;
        }

        await _workoutRepository.DeleteAsync(workoutId);

        return true;
    }

    private static WorkoutResponse MapToResponse(Workout workout)
    {
        return new WorkoutResponse
        {
            Id = workout.Id,
            Type = workout.Type,
            DateTime = workout.DateTime,
            DurationMinutes = workout.DurationMinutes,
            CaloriesBurned = workout.CaloriesBurned,
            IntensityLevel = workout.IntensityLevel,
            FatigueLevel = workout.FatigueLevel,
            Notes = workout.Notes
        };
    }

    public async Task<IEnumerable<WorkoutProgressResponse>> GetProgressAsync(
        Guid userId,
        int year,
        int month)
    {
        var workouts = await _workoutRepository.GetByUserIdAsync(userId);

        var monthWorkouts = workouts.Where(w => w.DateTime.Year == year && w.DateTime.Month == month).ToList();

        var progress = monthWorkouts.GroupBy(w => GetWeekOfMonth(w.DateTime)).Select(group => new WorkoutProgressResponse
        {
            Week = group.Key,
            TotalDurationMinutes = group.Sum(w => w.DurationMinutes),

            WorkoutCount = group.Count(),

            AverageIntensityLevel = Math.Round(group.Average(w => w.IntensityLevel), 2),

            AverageFatigueLevel = Math.Round(group.Average(w => w.FatigueLevel), 2),
        })
            .OrderBy(x => x.Week).ToList();
            
        return progress;
    }

    private static int GetWeekOfMonth(DateTime date)
    {
        return (date.Day - 1) / 7 + 1;
    }
}

// we check workout.UserId != userId to ensure that the workout belongs to the user making the request.
// If it doesn't, we return false, indicating that the operation is not allowed.
//CRUD is done only if the workout belongs to the user making the request.