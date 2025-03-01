import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../Service/authService';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5256';

  constructor(private http: HttpClient, private authService: AuthService ) {}

  login(email: string, password: string): Observable<string> {
    const loginDto = { email, password };
    return this.http.post<string>(`${this.apiUrl}/accounts/login`, loginDto, { responseType: 'text' as 'json' });
  }

  getScheduleEntries(): Observable<{ scheduleEntries: any[] }> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<{ scheduleEntries: any[] }>(`${this.apiUrl}/schedule-entries/schedule/entries`, { headers });
    
  }

  getSubjects(): Observable<{ subjects: any[] }> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<{ subjects: any[] }>(`${this.apiUrl}/subjects`, { headers });
  }

}
