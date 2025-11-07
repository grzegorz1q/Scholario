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


    getScheduleEntries(): Observable<{ scheduleEntries: any[] }> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
      return this.http.get<{ scheduleEntries: any[] }>(`${this.apiUrl}/schedule-entries/schedule/entries`, { headers });
    }

    createScheduleEntry(entry: ScheduleEntry): Observable<any> {
      return this.http.post(`${this.apiUrl}/schedule-entries/schedule/create`, entry);
    }

    getAllLessonHours(): Observable<LessonHour[]>{
      return this.http.get<LessonHour[]>(`${this.apiUrl}/lesson-hours`);
    }
  
    getSubjects(): Observable<{ subjects: any[] }> {    // Do sprawdzenia "getSubjects(): Observable<Subject[]> {"
      const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
      return this.http.get<{ subjects: any[] }>(`${this.apiUrl}/subjects`, { headers });
    }
  
}
