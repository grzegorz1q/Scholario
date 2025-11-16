import { Component, OnInit } from '@angular/core';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { ScheduleEntryService } from '../../Service/schedule-entry.service';
import { LessonHour } from '../Type/LessonHour';
import { Subject } from '../Type/Subject';
import { ScheduleEntry } from '../Type/ScheduleEntry';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-timetable',
  imports: [DragDropModule, CommonModule],
  templateUrl: './timetable.component.html',
  styleUrl: './timetable.component.scss'
})

export class TimetableComponent implements OnInit {
  days = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  lessonHours: LessonHour[] = [];
  subjects: Subject[] = [];
  scheduleEntries: ScheduleEntry[] = [];
  dayDropLists: string[] = [];

  constructor(private scheduleService: ScheduleEntryService) { }

  ngOnInit() {
    this.loadData();
    this.generateDropListIds();
  }

  generateDropListIds() {
    this.dayDropLists = [];
    for (let d = 0; d < this.days.length; d++) {
      for (let h = 0; h < this.lessonHours.length; h++) {
        this.dayDropLists.push(`dropList-${d}-${h + 1}`);
      }
    }
  }

loadData() {
  this.scheduleService.getAllLessonHours().subscribe(hours => {
    this.lessonHours = hours;
    this.generateDropListIds();
  });

  this.scheduleService.getSubjects().subscribe(r => this.subjects = r.subjects);
  this.scheduleService.getScheduleEntries().subscribe(r => this.scheduleEntries = r);
}


  drop(event: CdkDragDrop<any>, dayIndex: number, lessonNumber: number) {
    console.log('Przeciągnięto element:', event, dayIndex, lessonNumber);
    if (event.previousContainer === event.container) return;

    const subject = event.previousContainer.data[event.previousIndex];
    this.scheduleService.createScheduleEntry({
      teacherName: subject.teacherName,
      subjectId: subject.id,
      subjectName: subject.name,
      groupId: 1,
      day: dayIndex + 1,
      lessonNumber: lessonNumber
    }).subscribe(() => this.loadData());
  }
  

  getSubjectName(day: number, lessonNumber: number): string {
    const entry = this.scheduleEntries.find(
      e => e.day === day && e.lessonNumber === lessonNumber
    );
    return entry ? entry.subjectName : '';
  }

  //getalllessonhour - - trzeba zrobic

}

