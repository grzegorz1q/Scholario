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
  scheduleEntries: ScheduleEntry[] = [];  
  lessonHours: LessonHour[] = [];
  subjectsMap = new Map<number, Subject>();
  days: string[] = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  scheduleTable: (ScheduleEntry | undefined)[][] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(){
    this.loadData();
  }

  loadData(){
    this.apiService.getAllLessonHours().subscribe({
      next: lessonHours => {
        this.lessonHours = lessonHours;
        
        this.apiService.getLoggedUserScheduleEntries().subscribe({
          next: entries => {
            this.scheduleEntries = entries;
            this.buildScheduleTable();
          },
          error: error => console.error(error)
        });
      },
      error: error => console.error(error)
    });
  }

  buildScheduleTable(){
    this.scheduleTable = this.lessonHours.map(lessonHour => 
      this.days.map(day => 
        this.getScheduleEntry(day, lessonHour.lessonNumber)
      )
    );
  }
  getScheduleEntry(day: string, lesson: number): ScheduleEntry | undefined{
    const dayIndex = this.days.indexOf(day)+1; 
    const entry = this.scheduleEntries.find(e => e.day === dayIndex && e.lessonNumber === lesson);
    return entry ? entry : undefined;
  }
}
