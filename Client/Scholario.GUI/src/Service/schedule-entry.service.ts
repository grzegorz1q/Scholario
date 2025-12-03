import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthService } from '../Service/authService';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from '../app/Type/Subject'
import { ScheduleEntry } from '../app/Type/ScheduleEntry';
import { LessonHour } from '../app/Type/LessonHour';

@Injectable({
  providedIn: 'root'
})
export class ScheduleEntryService {

  private apiUrl = 'http://localhost:5256';

  constructor(private http: HttpClient, private authService: AuthService) { }


  getScheduleEntries(): Observable<ScheduleEntry[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<ScheduleEntry[]>(`${this.apiUrl}/schedule-entries`, { headers });
  }

  createScheduleEntry(entry: ScheduleEntry): Observable<ScheduleEntry> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.post<ScheduleEntry>(`${this.apiUrl}/schedule-entries/schedule/create`, entry, { headers, responseType: 'text' as 'json' });
  }

  createScheduleEntries(entry: ScheduleEntry[]): Observable<ScheduleEntry> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.post<ScheduleEntry>(`${this.apiUrl}/schedule-entries/schedule/creates`, entry, { headers, responseType: 'text' as 'json' });
  }

  getAllLessonHours(): Observable<LessonHour[]> {
    return this.http.get<LessonHour[]>(`${this.apiUrl}/lesson-hours`);
  }

  getSubjectsByGroupId(id: number): Observable<Subject[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<Subject[]>(`${this.apiUrl}/subjects/group/${id}`, { headers });
  }

  getScheduleByGroupId(id: number): Observable<ScheduleEntry[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    return this.http.get<ScheduleEntry[]>(`${this.apiUrl}/schedule-entries/group/${id}`, { headers });
  }
  getFilteredScheduleEntries(groupId?: number, teacherId?: number, classroomId?: number): Observable<ScheduleEntry[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
    let params = new HttpParams();

    if (groupId) params = params.set('groupId', groupId);
    if (teacherId) params = params.set('teacherId', teacherId);
    if (classroomId) params = params.set('classroomId', classroomId);

    return this.http.get<ScheduleEntry[]>(`${this.apiUrl}/schedule-entries/filter`, { headers, params });
  }
  save(entryOrEntries: ScheduleEntry | ScheduleEntry[]) {
    if (Array.isArray(entryOrEntries)) {
      return this.createScheduleEntries(entryOrEntries);
    }
    return this.createScheduleEntry(entryOrEntries);
  }
}
