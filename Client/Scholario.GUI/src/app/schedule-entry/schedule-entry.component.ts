import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../Service/api.service';

@Component({
  selector: 'app-schedule-entry',
  templateUrl: './schedule-entry.component.html',
  styleUrls: ['./schedule-entry.component.scss']
})
export class ScheduleEntryComponent implements OnInit {
  scheduleEntries: any[] = [];  // Zmienna do przechowywania danych

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    // Wywołanie metody serwisu, aby pobrać dane
    this.apiService.getScheduleEntries().subscribe(
      (data) => {
        this.scheduleEntries = data;  // Przypisanie danych do zmiennej
        console.log(this.scheduleEntries);  // Opcjonalnie: wyświetlenie w konsoli
      },
      (error) => {
        console.error('Błąd podczas pobierania danych:', error);  // Obsługa błędów
      }
    );
  }
}
