import { Component } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';

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

  constructor(private apiService: ApiService, private router: Router) {}

  login(): void {
    const loginDto = {
      email: this.email,
      password: this.password
    };

    this.apiService.login(loginDto.email, loginDto.password).subscribe(
      (response: string) => {
        console.log('Token:', response);
        localStorage.setItem('auth_token', response);
        this.router.navigate(['/schedule'])
      },
      
      (error: HttpErrorResponse) => {
        this.errorMessage = 'Błąd logowania: ' + error.message;
        console.error(error);
      }
    );
  }
}
