import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { WorkoutProgress } from '../../features/progress/progress.model';

@Injectable({
  providedIn: 'root',
})
export class ProgressService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'https://localhost:7152/api/workout/progress';

  getProgress(year: number, month: number): Observable<WorkoutProgress[]> {
    return this.http.get<WorkoutProgress[]>(`${this.apiUrl}?year=${year}&month=${month}`);
  }
}
