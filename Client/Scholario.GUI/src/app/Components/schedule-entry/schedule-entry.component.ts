import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScheduleEntry } from '../../Type/ScheduleEntry';
import { LessonHour } from '../../Type/LessonHour';
import { ScheduleEntryService } from '../../../Service/schedule-entry.service';
import { AuthService } from '../../../Service/authService';
import { DayOfWeek } from '../../Type/DayOfWeek';
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

  constructor(private scheduleService: ScheduleEntryService, private authService: AuthService) { }

  children: Student[] = [];
  childColors: Map<number, string> = new Map();
  colorPalette: string[] = [
    '#FF6B6B', '#4ECDC4', '#45B7D1', '#96CEB4', '#FFEAA7',
    '#DDA0DD', '#98D8C8', '#F7DC6F', '#BB8FCE', '#85C1E9'
  ]


  ngOnInit() {
    this.role = this.authService.getUserRole();
    this.loadData();
  }


  loadData() {
    this.scheduleService.getAllLessonHours().subscribe({
      next: lessonHours => {
        this.lessonHours = lessonHours;

        this.scheduleService.getScheduleEntries().subscribe({
          next: entries => {
            this.scheduleEntries = entries.map(e => ({
              ...e,
              day: DayOfWeek[e.day as unknown as keyof typeof DayOfWeek] as DayOfWeek
            }));
            console.table(this.scheduleEntries);
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


  initializeChildrenData() {
    const childrenMap = new Map<number, Student>();
    this.scheduleEntries.forEach(entry => {
      if (entry.studentId && entry.studentName && !childrenMap.has(entry.studentId)) {
        const nameArr = entry.studentName.split(' ');
        const child: Student = {
          id: entry.studentId,
          firstName: nameArr[0],
          lastName: nameArr[1],
          grades: []
        }
        childrenMap.set(entry.studentId, child);
      }
    });
    this.children = Array.from(childrenMap.values());

    this.children.forEach((child, index) => {
      this.childColors.set(child.id, this.colorPalette[index % this.colorPalette.length]);
    })
  }

  buildScheduleTable() {
    this.scheduleTable = this.lessonHours.map(lessonHour =>
      this.days.map(day =>
        this.getScheduleEntry(day, lessonHour.lessonNumber)
      )
    );
    console.log(this.scheduleTable);
  }

  getScheduleEntry(day: string, lesson: number): ScheduleEntry[] {
    const dayIndex = this.days.indexOf(day);
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
          classroomNumber: entry.classroomNumber,
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

  getTeacherNameInitials(teacherName: string): string {
    const namArr = teacherName.split(' ');
    return `${namArr[0].charAt(0)} ${namArr[1].charAt(0)}`;
  }
}
