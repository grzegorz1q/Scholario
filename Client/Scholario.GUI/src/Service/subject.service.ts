import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../Service/authService';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../app/Type/Student';
import { Subject } from '../app/Type/Subject';
import { SubjectCreateDto } from '../app/Type/SubjectCreateDto';


@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private apiUrl = 'http://localhost:5256';

  constructor(private http: HttpClient, private authService: AuthService) { }

  getStudentsBySubjectGroup(subjectId: number, groupId: number): Observable<Student[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<Student[]>(`${this.apiUrl}/teachers/subjects/${subjectId}/groups/${groupId}/students`, { headers });
  }

  getSubjects(): Observable<{ subjects: Subject[] }> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<{ subjects: Subject[] }>(`${this.apiUrl}/subjects`, { headers });
  }

  createSubject(subject: SubjectCreateDto): Observable<Subject> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.post<Subject>(`${this.apiUrl}/subjects`, { headers });
  }
}
