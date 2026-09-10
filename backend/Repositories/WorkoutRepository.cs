using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories.Interfaces;

namespace WorkoutTracker.Repositories;
public class WorkoutRepository : IWorkoutRepository
{
    private readonly AppDbContext _context;

    public WorkoutRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Workouts.Where(w => w.UserId == userId).ToListAsync();
    }

    public async Task<Workout?> GetByIdAsync(Guid id)
    {
        return await _context.Workouts.FindAsync(id);
    }

    public async Task AddAsync(Workout workout)
    {
        await _context.Workouts.AddAsync(workout);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Workout workout)
    {
        _context.Workouts.Update(workout);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var workout = await _context.Workouts.FindAsync(id);
        if (workout != null)
        {
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
        }
    }
}

