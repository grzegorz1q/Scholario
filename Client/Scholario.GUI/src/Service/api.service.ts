import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../Service/authService';
import { Grade } from '../app/Type/Grade';
import { StudentGrade } from '../app/Type/StudentGrade';
import { Student } from '../app/Type/Student';

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

  getSubjects(): Observable<{ subjects: any[] }> {    // Do sprawdzenia "getSubjects(): Observable<Subject[]> {"
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<{ subjects: any[] }>(`${this.apiUrl}/subjects`, { headers });
  }

  getGradeByStudent(): Observable<StudentGrade[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<StudentGrade[]>(`${this.apiUrl}/students/grade`, { headers });
  }

  getStudentsBySubjectGroup(subjectId: number, groupId: number): Observable<Student[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<Student[]>(`${this.apiUrl}/teachers/subjects/${subjectId}/groups/${groupId}/students`, { headers });
  }
  
  addGrade(grade: Grade): Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.post(`${this.apiUrl}/grades`, grade,{ headers, responseType: 'text' as 'json' });
  }
}
