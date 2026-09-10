import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Workout } from '../../features/workouts/workout.model';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  private readonly apiUrl = 'https://localhost:7152/api/workout';

  constructor(private http: HttpClient) {}

  getWorkouts(): Observable<Workout[]> {
    return this.http.get<Workout[]>(this.apiUrl);
  }

  getWorkout(id: string): Observable<Workout> {
    return this.http.get<Workout>(`${this.apiUrl}/${id}`);
  }

  createWorkout(workout: Omit<Workout, 'id'>): Observable<Workout> {
    return this.http.post<Workout>(this.apiUrl, workout);
  }

  updateWorkout(id: string, workout: Omit<Workout, 'id'>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, workout);
  }

  deleteWorkout(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
