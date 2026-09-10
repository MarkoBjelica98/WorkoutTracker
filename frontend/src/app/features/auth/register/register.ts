import { Component, inject } from '@angular/core';

import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';

import { Router } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
  ],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  private authService = inject(AuthService);
  private snackBar = inject(MatSnackBar);

  registerForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required]],
  });

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const password = this.registerForm.value.password;
    const confirmPassword = this.registerForm.value.confirmPassword;

    if (password !== confirmPassword) {
      this.registerForm.controls.confirmPassword.setErrors({
        passwordMismatch: true,
      });

      this.snackBar.open('Passwords do not match.', 'Close', {
        duration: 3000,
      });

      return;
    }

    const email = this.registerForm.value.email;

    this.authService
      .register({
        email: email!,
        password: password!,
      })
      .subscribe({
        next: () => {
          this.snackBar.open('Registration successful.', 'Close', {
            duration: 3000,
          });

          this.router.navigate(['/login']);
        },

        error: (error) => {
          console.error('Registration failed:', error);

          this.snackBar.open('Registration failed. Please try again.', 'Close', {
            duration: 3000,
          });
        },
      });
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }
}
