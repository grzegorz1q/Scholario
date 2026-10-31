import { Component } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../../Service/authService';

@Component({
  selector: 'app-login',
  imports: [FormsModule, NgIf],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private apiService: ApiService, private authService: AuthService, private router: Router) { }

  login(): void {
    const loginDto = {
      email: this.email,
      password: this.password
    };

    this.apiService.login(loginDto.email, loginDto.password).subscribe(
      (response: string) => {
        //console.log('Token:', response);
        localStorage.setItem('auth_token', response);

        if (this.authService.isAdmin()) {
          this.router.navigate(['/timetable']);
        } else if (this.authService.isTeacher()) {
          this.router.navigate(['/schedule']);
        } else if (this.authService.isStudent()) {
          this.router.navigate(['schedule']);
        }
        else {
          this.router.navigate(['/home']);
        }
      },

      (error: HttpErrorResponse) => {
        this.errorMessage = 'Niepoprawny email lub hasło';
        console.error(error);
      }
    );
  }
}
