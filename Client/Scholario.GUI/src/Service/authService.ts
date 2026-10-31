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

  getToken(): string | null {
    return localStorage.getItem('auth_token');
  }

  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
  
    try {
      // Dekodowanie payloadu JWT
      const payload = JSON.parse(atob(token.split('.')[1]));
      console.log(payload); // Zobacz, co jest w payloadzie
  
      // Sprawdzenie roli w niestandardowym kluczu
      const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      return role || null;
    } catch (e) {
      console.error("Błąd dekodowania tokena", e);
      return null;
    }
  }
  

  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }
  isTeacher(): boolean {
    return this.getUserRole() === 'Teacher';
  }
  isStudent(): boolean {
    return this.getUserRole() === 'Student';
  }
  isParent(): boolean {
    return this.getUserRole() === 'Parent';
  }
}
