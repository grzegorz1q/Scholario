import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../Service/authService';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Grade } from '../app/Type/Grade';
import { StudentGrade } from '../app/Type/StudentGrade';

@Injectable({
  providedIn: 'root'
})
export class GradeService {

  private apiUrl = 'http://localhost:5256';
  constructor(private http: HttpClient, private authService: AuthService) { }

  addGrade(grade: Grade): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.post(`${this.apiUrl}/grades`, grade, { headers, responseType: 'text' as 'json' });
  }

  getGradeByStudent(): Observable<StudentGrade[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<StudentGrade[]>(`${this.apiUrl}/students/grade`, { headers });
  }
}
