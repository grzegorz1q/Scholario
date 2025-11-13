import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { CommonModule } from '@angular/common';
import { Subject } from '../../Type/Subject';
import { ScheduleEntry } from '../../Type/ScheduleEntry';
import { LessonHour } from '../../Type/LessonHour';

@Component({
  selector: 'app-schedule-entry',
  imports: [CommonModule],
  templateUrl: './schedule-entry.component.html',
  styleUrls: ['./schedule-entry.component.scss']
})

export class ScheduleEntryComponent implements OnInit {
  selectedSubject: any = null;
  scheduleEntries: ScheduleEntry[] = [];  
  lessonHours: LessonHour[] = [];
  subjects : Subject[] = [];
  days: string[] = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  schedule: string[][] = [];
  
  constructor(private apiService: ApiService) {}

  ngOnInit(){
    this.getAllLessonHours();
    this.getSubject();
    this.getLoggedUserScheduleEntries();
  }

  getAllLessonHours(){
    this.apiService.getAllLessonHours().subscribe({
      next: (response) => {
        this.lessonHours = response;
      },
      error: (error) => {
        console.error(error);
      }
    })
  }
  getLoggedUserScheduleEntries(){
    this.apiService.getLoggedUserScheduleEntries().subscribe({
      next: (response) => {
        this.scheduleEntries = response;
      },
      error: error => { 
        console.error('Błąd podczas pobierania planu zajęć:', error)
      }
    });
  }
  getScheduleEntry(day: string, lesson: number): string {
    const dayIndex = this.days.indexOf(day)+1; 
    const entry = this.scheduleEntries.find(e => e.day === dayIndex && e.lessonNumber === lesson);
    return entry ? entry.subjectName : "-";
  }

  getSubject() {
    this.apiService.getSubjects().subscribe({
      next: (response) => {
        this.subjects = response.subjects;
      },
      error: (error) => {
        console.error('Błąd podczas pobierania przedmiotów:', error)
      }
    });
  }
  
  getSubjectId(day: string, lesson: number): number | null {
    const dayIndex = this.days.indexOf(day) + 1;
    const entry = this.scheduleEntries.find(e => e.day === dayIndex && e.lessonNumber === lesson);
    return entry ? entry.subjectId : null;
  }

  
  showSubjectDetails(subjectId: number | null): void {
    if (!subjectId) return;
    
    this.apiService.getSubjects().subscribe(
      response => {
        console.log(response);
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
