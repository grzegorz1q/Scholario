import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subject } from '../../Type/Subject';
import { ScheduleEntry } from '../../Type/ScheduleEntry';
import { LessonHour } from '../../Type/LessonHour';
import { ScheduleEntryService } from '../../../Service/schedule-entry.service';
import { AuthService } from '../../../Service/authService';

@Component({
  selector: 'app-schedule-entry',
  imports: [CommonModule],
  templateUrl: './schedule-entry.component.html',
  styleUrls: ['./schedule-entry.component.scss']
})

export class ScheduleEntryComponent implements OnInit {
  scheduleEntries: ScheduleEntry[] = [];  
  lessonHours: LessonHour[] = [];
  days: string[] = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  scheduleTable: ScheduleEntry[][][] = [];
  role: string | null = null;

  constructor(private scheduleService: ScheduleEntryService, private authService: AuthService) {}

  ngOnInit(){
    this.role = this.authService.getUserRole();
    this.loadData();
  }
  
  
  loadData(){
    this.scheduleService.getAllLessonHours().subscribe({
      next: lessonHours => {
        this.lessonHours = lessonHours;
        
        this.scheduleService.getScheduleEntries().subscribe({
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
    console.log(this.scheduleTable);
  }
  getScheduleEntry(day: string, lesson: number): ScheduleEntry[]{
    const dayIndex = this.days.indexOf(day)+1; 
    return this.scheduleEntries.filter(e => e.day === dayIndex && e.lessonNumber === lesson);
  }

}
