import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './authService';
import { Observable } from 'rxjs';
import { Classroom } from '../app/Type/Classroom';

@Injectable({
  providedIn: 'root'
})
export class ClassroomService {

    private apiUrl = 'http://localhost:5256';
    constructor(private http: HttpClient, private authService: AuthService) { }
  
    getClassrooms(): Observable<Classroom[]>{
          const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
          return this.http.get<Classroom[]>(`${this.apiUrl}/classrooms`, { headers });
    }
}
