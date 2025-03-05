import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { CommonModule } from '@angular/common';
import { Subject } from '../../Type/Subject';
import { ScheduleEntry } from '../../Type/ScheduleEntry';

@Component({
  selector: 'app-schedule-entry',
  imports: [CommonModule],
  templateUrl: './schedule-entry.component.html',
  styleUrls: ['./schedule-entry.component.scss']
})

export class ScheduleEntryComponent implements OnInit {
  selectedSubject: any = null;
  scheduleEntries: ScheduleEntry[] = [];  
  subjects : Subject[] = [];
  days: string[] = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  lessons: number[] = [1, 2, 3, 4, 5, 6, 7, 8];
  schedule: string[][] = [];
  
  constructor(private apiService: ApiService) {}

  ngOnInit(){
    this.getSubject();
    this.getScheduleEntries();
  }


  getScheduleEntries(){
    this.apiService.getScheduleEntries().subscribe(
      data => {
        this.scheduleEntries = data.scheduleEntries;
      },
      error => console.error('Błąd podczas pobierania planu zajęć:', error)
    );
  }

  getSubject() {
    this.apiService.getSubjects().subscribe(
      response => {
        this.subjects = response.subjects;
      },
      error => console.error('Błąd podczas pobierania przedmiotów:', error)
    );
  }
  
  getSubjectId(day: string, lesson: number): number | null {
    const dayIndex = this.days.indexOf(day) + 1;
    const entry = this.scheduleEntries.find(e => e.day === dayIndex && e.lessonNumber === lesson);
    return entry ? entry.subjectId : null;
  }
  
  getScheduleEntry(day: string, lesson: number): string {
    const dayIndex = this.days.indexOf(day) + 1; 
    const entry = this.scheduleEntries.find(e => e.day === dayIndex && e.lessonNumber === lesson);
    return entry ? entry.subjectName : "-";
  }

  
  showSubjectDetails(subjectId: number | null): void {
    if (!subjectId) return;
    
    this.apiService.getSubjects().subscribe(
      response => {
        const subject = response.subjects.find(s => s.id === subjectId);
        if (subject) {
          this.selectedSubject = subject;
        } else {
          console.error('Nie znaleziono przedmiotu o ID:', subjectId);
        }
      },
      error => console.error('Błąd podczas pobierania przedmiotów:', error)
    );
  }
}
