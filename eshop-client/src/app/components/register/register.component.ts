import { Component } from '@angular/core';
import { AuthService, RegisterUserDTO } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerUserDTO: RegisterUserDTO = {
    username: '',
    email: '',
    password: '',
    role: 'User'
  };

  confirmPassword: string = ''; // Добавлено для подтверждения пароля
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    if (this.registerUserDTO.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    this.authService.register(this.registerUserDTO).subscribe({
      next: (response: string) => {
        console.log('Registration successful', response);
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Registration failed', err);
        this.errorMessage = 'Registration failed. Please try again.';
      }
    });
  }
}
