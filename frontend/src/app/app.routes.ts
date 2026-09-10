import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';

import { WorkoutList } from './features/workouts/workout-list/workout-list';
import { Progress } from './features/progress/progress';
import { WorkoutForm } from './features/workouts/workout-form/workout-form';

import { Layout } from './shared/layout/layout';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },

  {
    path: 'login',
    component: Login,
  },

  {
    path: 'register',
    component: Register,
  },

  {
    path: '',
    component: Layout,
    canActivate: [authGuard],
    children: [
      {
        path: 'workouts',
        component: WorkoutList,
      },

      {
        path: 'progress',
        component: Progress,
      },

      {
        path: 'workouts/new',
        component: WorkoutForm,
      },

      {
        path: 'workouts/edit/:id',
        component: WorkoutForm,
      },
    ],
  },
];
