import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subject } from '../../Type/Subject';
import { ScheduleEntry } from '../../Type/ScheduleEntry';
import { LessonHour } from '../../Type/LessonHour';
import { ScheduleEntryService } from '../../../Service/schedule-entry.service';

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
  
  constructor(private scheduleService: ScheduleEntryService) {}

  ngOnInit(){
    this.getAllLessonHours();
    this.getSubject();
    this.getScheduleEntries();
  }

  getAllLessonHours(){
    this.scheduleService.getAllLessonHours().subscribe({
      next: (response) => {
        this.lessonHours = response;
      },
      error: (error) => {
        console.error(error);
      }
    })
  }
  getScheduleEntries(){
    this.scheduleService.getScheduleEntries().subscribe(
      data => {
        this.scheduleEntries = data.scheduleEntries;
      },
      error => console.error('Błąd podczas pobierania planu zajęć:', error)
    );
  }

    loadScheduleEntries() {
    this.scheduleService.getScheduleEntries().subscribe({
      next: (data) => this.scheduleEntries = data.scheduleEntries,
      error: (err) => console.error('Błąd przy pobieraniu planu:', err)
    });
  }

  getSubject() {
    this.scheduleService.getSubjects().subscribe(
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
    
    this.scheduleService.getSubjects().subscribe(
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
