using FluentValidation;
using WorkoutTracker.DTOs.Workout;

namespace WorkoutTracker.Validators;

public class CreateWorkoutRequestValidator : AbstractValidator<CreateWorkoutRequest>
{
    public CreateWorkoutRequestValidator()
    {
        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("Workout duration must be greater than 0 minutes.");

        RuleFor(x => x.CaloriesBurned)
            .GreaterThanOrEqualTo(0).WithMessage("Calories burned cannot be negative.");

        RuleFor(x => x.IntensityLevel)
            .InclusiveBetween(1, 10).WithMessage("Intensity level must be between 1 and 10.");

        RuleFor(x => x.FatigueLevel)
            .InclusiveBetween(1, 10).WithMessage("Fatigue level must be between 1 and 10.");

        RuleFor(x => x.DateTime)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Workout date and time cannot be in the future.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid workout type.");
    }
}
