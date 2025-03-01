import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5256';

  constructor(private http: HttpClient) {}

  getScheduleEntries(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/schedule-entries/schedule/entries`);
  }
}