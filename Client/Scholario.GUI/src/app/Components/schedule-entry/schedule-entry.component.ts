import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScheduleEntry } from '../../Type/ScheduleEntry';
import { LessonHour } from '../../Type/LessonHour';
import { ScheduleEntryService } from '../../../Service/schedule-entry.service';
import { AuthService } from '../../../Service/authService';
import { ParentLegendComponent } from "./parent-legend/parent-legend.component";
import { Student } from '../../Type/Student';

@Component({
  selector: 'app-schedule-entry',
  imports: [CommonModule, ParentLegendComponent],
  templateUrl: './schedule-entry.component.html',
  styleUrls: ['./schedule-entry.component.scss']
})

export class ScheduleEntryComponent implements OnInit {
  scheduleEntries: ScheduleEntry[] = [];  
  lessonHours: LessonHour[] = [];
  days: string[] = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  scheduleTable: ScheduleEntry[][][] = [];
  role: string | null = null;

  children: Student[] = [];
  childColors: Map<number, string> = new Map();
  colorPalette: string[] = [
    '#FF6B6B', '#4ECDC4', '#45B7D1', '#96CEB4', '#FFEAA7',
    '#DDA0DD', '#98D8C8', '#F7DC6F', '#BB8FCE', '#85C1E9'
  ]

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
            if (this.role === 'Parent') {
              this.initializeChildrenData();
            }
            this.buildScheduleTable();
          },
          error: error => console.error(error)
        });
      },
      error: error => console.error(error)
    });
  }

  initializeChildrenData(){
    const uniqueChildren = new Map<number, any>();
    this.scheduleEntries.forEach(entry => {
      if(entry.studentId && entry.studentName){
        if(!uniqueChildren.has(entry.studentId)){
          uniqueChildren.set(entry.studentId,{
            id: entry.studentId,
            name: entry.studentName
          });
        }
    }
    });
    this.children = Array.from(uniqueChildren.values());
    this.children.forEach((child, index) => {
      this.childColors.set(child.id, this.colorPalette[index % this.colorPalette.length]);
    })
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

  // Grupuj wpisy według grupy dla rodzica
  getGroupedEntries(entries: ScheduleEntry[]): any[] {
    if (this.role !== 'Parent') {
      return entries;
    }

    const grouped = new Map<string, any>();
    
    entries.forEach(entry => {
      if (!entry.studentId || !entry.studentName) {
        return;
      }
      const key = `${entry.subjectId}-${entry.groupId}`;
      
      if (!grouped.has(key)) {
        grouped.set(key, {
          subjectName: entry.subjectName,
          groupName: entry.groupName,
          teacherName: entry.teacherName,
          students: []
        });
      }
      
      const group = grouped.get(key);
      group.students.push({
        id: entry.studentId,
        name: entry.studentName,
        color: this.childColors.get(entry.studentId)
      });
    });
    
    return Array.from(grouped.values());
  }

  getChildColor(studentId: number): string {
    return this.childColors.get(studentId) || '#CCCCCC';
  }

  getTeacherNameInitials(teacherName: string): string{
    const namArr = teacherName.split(' ');
    return `${namArr[0].charAt(0)} ${namArr[1].charAt(0)}`;
  }
}
