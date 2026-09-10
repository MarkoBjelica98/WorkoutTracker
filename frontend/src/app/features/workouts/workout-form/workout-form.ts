import { Component, inject, OnInit } from '@angular/core';

import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';

import { Router, ActivatedRoute } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { WorkoutService } from '../../../core/services/workout.service';

@Component({
  selector: 'app-workout-form',
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSnackBarModule,
  ],
  templateUrl: './workout-form.html',
  styleUrl: './workout-form.css',
})
export class WorkoutForm implements OnInit {
  private formBuilder = inject(FormBuilder);
  private workoutService = inject(WorkoutService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private snackBar = inject(MatSnackBar);

  isEditMode = false;
  workoutId: string | null = null;

  maxDateTime = this.getCurrentDateTime();

  workoutForm = this.formBuilder.group({
    type: [0, [Validators.required]],
    dateTime: ['', [Validators.required]],
    durationMinutes: [30, [Validators.required, Validators.min(1)]],
    caloriesBurned: [0, [Validators.required, Validators.min(0)]],
    intensityLevel: [5, [Validators.required, Validators.min(1), Validators.max(10)]],
    fatigueLevel: [5, [Validators.required, Validators.min(1), Validators.max(10)]],
    notes: ['', [Validators.maxLength(500)]],
  });

  private getCurrentDateTime(): string {
    const now = new Date();

    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  ngOnInit(): void {
    this.workoutId = this.route.snapshot.paramMap.get('id');

    if (!this.workoutId) {
      return;
    }

    this.isEditMode = true;

    this.workoutService.getWorkout(this.workoutId).subscribe({
      next: (workout) => {
        this.workoutForm.patchValue({
          type: workout.type,
          dateTime: workout.dateTime.slice(0, 16),
          durationMinutes: workout.durationMinutes,
          caloriesBurned: workout.caloriesBurned,
          intensityLevel: workout.intensityLevel,
          fatigueLevel: workout.fatigueLevel,
          notes: workout.notes ?? '',
        });
      },
      error: (error) => {
        console.error('Failed to load workout:', error);

        this.snackBar.open('Failed to load workout.', 'Close', {
          duration: 3000,
        });
      },
    });
  }

  onSubmit(): void {
    if (this.workoutForm.invalid) {
      this.workoutForm.markAllAsTouched();
      return;
    }

    const formValue = this.workoutForm.getRawValue();

    if (formValue.dateTime) {
      const selectedDateTime = new Date(formValue.dateTime);
      const currentDateTime = new Date();

      if (selectedDateTime > currentDateTime) {
        this.workoutForm.controls.dateTime.setErrors({
          futureDate: true,
        });

        this.snackBar.open('Workout date and time cannot be in the future.', 'Close', {
          duration: 3000,
        });

        return;
      }
    }

    const workout = {
      type: formValue.type as 0 | 1 | 2,
      dateTime: formValue.dateTime!,
      durationMinutes: formValue.durationMinutes!,
      caloriesBurned: formValue.caloriesBurned!,
      intensityLevel: formValue.intensityLevel!,
      fatigueLevel: formValue.fatigueLevel!,
      notes: formValue.notes || undefined,
    };

    if (this.isEditMode && this.workoutId) {
      this.workoutService.updateWorkout(this.workoutId, workout).subscribe({
        next: () => {
          this.router.navigate(['/workouts']);
        },
        error: (error) => {
          console.error('Failed to update workout:', error);

          this.snackBar.open('Failed to update workout.', 'Close', {
            duration: 3000,
          });
        },
      });

      return;
    }

    this.workoutService.createWorkout(workout).subscribe({
      next: () => {
        this.router.navigate(['/workouts']);
      },
      error: (error) => {
        console.error('Failed to create workout:', error);

        this.snackBar.open('Failed to create workout.', 'Close', {
          duration: 3000,
        });
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/workouts']);
  }
}
