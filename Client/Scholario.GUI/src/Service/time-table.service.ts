import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../Service/authService';

@Injectable({
  providedIn: 'root'
})
export class TimeTableService {

  private apiUrl = 'http://localhost:5256';
  
  constructor(private http:HttpClient, private authService: AuthService) { }


}
