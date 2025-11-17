import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../Service/authService';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ScheduleEntry } from '../app/Type/ScheduleEntry';
import { LessonHour } from '../app/Type/LessonHour';

@Injectable({
  providedIn: 'root'
})
export class ScheduleEntryService {

  private apiUrl = 'http://localhost:5256';

  constructor(private http:HttpClient, private authService: AuthService) { }


    getScheduleEntries(): Observable<ScheduleEntry[]> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
      return this.http.get<ScheduleEntry[]>(`${this.apiUrl}/schedule-entries`, { headers });
    }

    createScheduleEntry(entry: ScheduleEntry): Observable<ScheduleEntry> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
      return this.http.post<ScheduleEntry>(`${this.apiUrl}/schedule-entries/schedule/create`, entry, { headers, responseType: 'text' as 'json' } );
    }

    getAllLessonHours(): Observable<LessonHour[]>{
      return this.http.get<LessonHour[]>(`${this.apiUrl}/lesson-hours`);
    }
  
    getSubjects(): Observable<{ subjects: any[] }> {    // Do sprawdzenia "getSubjects(): Observable<Subject[]> {"
      const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
      return this.http.get<{ subjects: any[] }>(`${this.apiUrl}/subjects/user`, { headers });
    }
  
}
