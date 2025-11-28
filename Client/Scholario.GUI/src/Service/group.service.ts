import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './authService';
import { Group } from '../app/Type/Group';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private apiUrl = 'http://localhost:5256';
  constructor(private http: HttpClient, private authService: AuthService) { }

  getAllGroups(): Observable<Group[]> {
      const headers = new HttpHeaders().set('Authorization', `Bearer ${this.authService.getToken()}`);
      return this.http.get<Group[]>(`${this.apiUrl}/groups`, { headers });
    }
}
