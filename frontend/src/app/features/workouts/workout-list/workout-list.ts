import { Component, OnInit, inject, signal } from '@angular/core';

import { DatePipe } from '@angular/common';

import { Router, RouterLink } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';

import { MatCardModule } from '@angular/material/card';

import { MatDialog, MatDialogModule } from '@angular/material/dialog';

import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { WorkoutService } from '../../../core/services/workout.service';

import { Workout } from '../workout.model';

import { ConfirmDialog, ConfirmDialogData } from '../../../shared/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-workout-list',
  imports: [
    MatButtonModule,
    MatCardModule,
    MatDialogModule,
    DatePipe,
    RouterLink,
    MatSnackBarModule,
  ],
  templateUrl: './workout-list.html',
  styleUrl: './workout-list.css',
})
export class WorkoutList implements OnInit {
  private readonly workoutService = inject(WorkoutService);
  private readonly router = inject(Router);
  private readonly snackBar = inject(MatSnackBar);
  private readonly dialog = inject(MatDialog);

  workouts = signal<Workout[]>([]);

  ngOnInit(): void {
    this.loadWorkouts();
  }

  loadWorkouts(): void {
    this.workoutService.getWorkouts().subscribe({
      next: (workouts) => {
        this.workouts.set(workouts);
      },
      error: (error) => {
        console.error('Failed to load workouts:', error);

        this.snackBar.open('Failed to load workouts.', 'Close', {
          duration: 3000,
        });
      },
    });
  }

  getWorkoutType(type: number): string {
    switch (type) {
      case 0:
        return 'Cardio';

      case 1:
        return 'Strength';

      case 2:
        return 'Flexibility';

      default:
        return 'Unknown';
    }
  }

  editWorkout(id: string): void {
    this.router.navigate(['/workouts/edit', id]);
  }

  deleteWorkout(id: string): void {
    const dialogData: ConfirmDialogData = {
      title: 'Delete Workout',
      message: 'Are you sure you want to delete this workout? This action cannot be undone.',
      confirmText: 'Delete',
      cancelText: 'Cancel',
    };

    const dialogRef = this.dialog.open(ConfirmDialog, {
      width: '400px',
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (!confirmed) {
        return;
      }

      this.workoutService.deleteWorkout(id).subscribe({
        next: () => {
          this.loadWorkouts();

          this.snackBar.open('Workout deleted successfully.', 'Close', {
            duration: 3000,
          });
        },
        error: (error) => {
          console.error('Failed to delete workout:', error);

          this.snackBar.open('Failed to delete workout.', 'Close', {
            duration: 3000,
          });
        },
      });
    });
  }
}
