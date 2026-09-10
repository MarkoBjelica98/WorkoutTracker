import { Component, inject, OnInit } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import { ProgressService } from '../../core/services/progress.service';
import { WorkoutProgress } from './progress.model';

@Component({
  selector: 'app-progress',
  imports: [MatButtonModule, MatCardModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './progress.html',
  styleUrl: './progress.css',
})
export class Progress implements OnInit {
  private readonly progressService = inject(ProgressService);

  progress: WorkoutProgress[] = [];

  selectedYear = new Date().getFullYear();
  selectedMonth = new Date().getMonth() + 1;

  months = [
    { value: 1, name: 'January' },
    { value: 2, name: 'February' },
    { value: 3, name: 'March' },
    { value: 4, name: 'April' },
    { value: 5, name: 'May' },
    { value: 6, name: 'June' },
    { value: 7, name: 'July' },
    { value: 8, name: 'August' },
    { value: 9, name: 'September' },
    { value: 10, name: 'October' },
    { value: 11, name: 'November' },
    { value: 12, name: 'December' },
  ];

  ngOnInit(): void {
    this.loadProgress();
  }

  loadProgress(): void {
    this.progressService.getProgress(this.selectedYear, this.selectedMonth).subscribe({
      next: (progress) => {
        this.progress = progress;
      },
      error: (error) => {
        console.error('Failed to load progress:', error);
      },
    });
  }

  onMonthChange(): void {
    this.loadProgress();
  }
}
