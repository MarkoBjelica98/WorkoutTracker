using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } // Tells EF Core to create a Users table in the database
    public DbSet<Workout> Workouts { get; set; } // Tells EF Core to create a Workouts table in the database
}
