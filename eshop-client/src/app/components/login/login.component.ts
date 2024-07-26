import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, LoginUserDTO } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginUserDTO: LoginUserDTO = {
    email: '',
    password: ''
  };

  errorMessage: string | null = null;

  constructor(private router: Router, private authService: AuthService) {}

  onSubmit() {
    this.authService.login(this.loginUserDTO).subscribe(response => {
      console.log(response);
      localStorage.setItem('token', response.token);
      this.router.navigate(['/welcome-page']);
    }, error => {
      console.error(error);
      this.errorMessage = 'Login failed. Please try again.';
    })
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}