export interface Workout {
  id: string;
  type: number;
  dateTime: string;
  durationMinutes: number;
  caloriesBurned: number;
  intensityLevel: number;
  fatigueLevel: number;
  notes?: string;
}
