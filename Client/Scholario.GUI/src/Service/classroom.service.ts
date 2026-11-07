import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './authService';
import { Classroom } from '../app/Type/Classroom';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

  private readonly apiUrl = 'http://localhost:5256';

  constructor(
    private readonly http: HttpClient,
    private readonly authService: AuthService
  ) { }

  getAllClassrooms(): Observable<Classroom[]> {
    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.authService.getToken()}`
    );

    return this.http.get<Classroom[]>(`${this.apiUrl}/classrooms`, { headers });
  }
}
