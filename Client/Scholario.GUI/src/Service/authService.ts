import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('auth_token');
    return token ? true : false;
  }

  getToken() {
    return localStorage.getItem('auth_token');
  }
}
