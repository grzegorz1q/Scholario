import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../Service/authService';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../app/Type/Student';
import { Subject } from '../app/Type/Subject';
import { SubjectCreateDto } from '../app/Type/SubjectCreateDto';
import { LoggedUserSubjects } from '../app/Type/LoggedUserSubjects';


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

  getLoggedUserSubjects(): Observable<LoggedUserSubjects>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<LoggedUserSubjects>(`${this.apiUrl}/subjects/user`, { headers });
  }

  getSubjects(): Observable<{ subjects: Subject[] }> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<{ subjects: Subject[] }>(`${this.apiUrl}/subjects`, { headers });
  }

  createSubject(subject: SubjectCreateDto): Observable<Subject> { //jak to dziala? czy jest gdzies wykorzystane? subject nie jest przekazywany
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.post<Subject>(`${this.apiUrl}/subjects`, { headers });
  }
}
