import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../Service/authService';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../app/Type/Student';
import { Subject } from '../app/Type/Subject';


@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private apiUrl = 'http://localhost:5256';
  
  constructor(private http:HttpClient, private authService: AuthService) { }

  getStudentsBySubjectGroup(subjectId: number, groupId: number): Observable<Student[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<Student[]>(`${this.apiUrl}/teachers/subjects/${subjectId}/groups/${groupId}/students`, { headers });
  }

  getSubjects(): Observable<Subject[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<Subject[]>(`${this.apiUrl}/subjects`, { headers });
  }
}
