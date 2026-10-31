import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './authService';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.authService.isAuthenticated()) {
      return true; // jeśli użytkownik jest zalogowany
    } else {
      this.router.navigate(['/login']); // jeśli nie, przekieruj na stronę logowania
      return false;
    }
  }

  getCurrentUserFromToken(): { role: string, email: string } | null {
    const token = localStorage.getItem('auth_token');
    if (!token) return null;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return { role: payload.role, email: payload.email };
    } catch {
      return null;
    }
  }
}
