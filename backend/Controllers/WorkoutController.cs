using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.DTOs.Workout;
using WorkoutTracker.Services.Interfaces;
using FluentValidation;

namespace WorkoutTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    private readonly IValidator<CreateWorkoutRequest> _createWorkoutValidator;

    private readonly IValidator<UpdateWorkoutRequest> _updateWorkoutValidator;

    public WorkoutController(
        IWorkoutService workoutService, 
        IValidator<CreateWorkoutRequest> createWorkoutValidator,
        IValidator<UpdateWorkoutRequest> updateWorkoutValidator)
    {
        _workoutService = workoutService;
        _createWorkoutValidator = createWorkoutValidator;
        _updateWorkoutValidator = updateWorkoutValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyWorkouts()
    {
        var userId = GetUserId();

        var workouts = await _workoutService.GetMyWorkoutsAsync(userId);

        return Ok(workouts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();

        var workout = await _workoutService.GetByIdAsync(userId, id);

        if (workout == null)
        {
            return NotFound();
        }

        return Ok(workout);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkoutRequest request)
    {
        var validationResult = await _createWorkoutValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var userId = GetUserId();

        var workout = await _workoutService.CreateAsync(userId, request);

        return CreatedAtAction(nameof(GetById), new { id = workout.Id }, workout);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateWorkoutRequest request)
    {
        var validationResult = await _updateWorkoutValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var userId = GetUserId();

        var updated = await _workoutService.UpdateAsync(userId, id, request);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();

        var delete = await _workoutService.DeleteAsync(userId, id);

        if (!delete)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("progress")]
    public async Task<IActionResult> GetProgress(
        [FromQuery] int year,
        [FromQuery] int month)
    {
        var userId = GetUserId();

        if (month < 1 || month > 12)
        {
            return BadRequest("Month must be between 1 and 12.");
        }

        var progress = await _workoutService.GetProgressAsync(userId, year, month);

        return Ok(progress);
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user ID from the JWT token claims

        return Guid.Parse(userId);
    }
}
