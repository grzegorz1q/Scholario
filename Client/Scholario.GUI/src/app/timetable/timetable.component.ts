import { Component, OnInit, signal } from '@angular/core';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { ScheduleEntryService } from '../../Service/schedule-entry.service';
import { SubjectService } from '../../Service/subject.service';
import { LessonHour } from '../Type/LessonHour';
import { Subject } from '../Type/Subject';
import { ScheduleEntry } from '../Type/ScheduleEntry';
import { CommonModule } from '@angular/common';
import { Group } from '../Type/Group';
import { GroupService } from '../../Service/group.service';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-timetable',
  imports: [DragDropModule, CommonModule, FormsModule],
  templateUrl: './timetable.component.html',
  styleUrl: './timetable.component.scss'
})

export class TimetableComponent implements OnInit {
  days = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  lessonHours: LessonHour[] = [];
  selectedGroupId : number | null = null;
  subjects: Subject[] = [];
  scheduleEntries: ScheduleEntry[] = [];
  dayDropLists: string[] = [];
  groups: Group[] = [];
  initialsOfName: string[] = [];

  constructor(private scheduleService: ScheduleEntryService, private subjectService: SubjectService, private groupService: GroupService) { }

  ngOnInit() {
    this.loadData();
    this.generateDropListIds();
    this.getAllGroups();
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

    this.subjectService.getSubjects().subscribe(r => this.subjects = r);
    this.scheduleService.getScheduleEntries().subscribe(r => this.scheduleEntries = r);
  }

  getAllGroups(){
    this.groupService.getAllGroups().subscribe({
      next: (groups) => {
        this.groups = groups;
      },
      error: (err) => {
        console.error(err)
      }
    });
  }

  drop(event: CdkDragDrop<any>, dayIndex: number, lessonNumber: number) {
    console.log('Przeciągnięto element:', event, dayIndex, lessonNumber);
    if (event.previousContainer === event.container) return;
    console.log(event.previousContainer)
    const subject = event.previousContainer.data[event.previousIndex];
    console.log(subject.id)
    this.scheduleService.createScheduleEntry({
      subjectId: subject.id,
      subjectName: subject.name,
      groupId: Number(this.selectedGroupId),
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

  getInitials(subject: Subject) {
    const names = subject.teacherName.split(' ');
    return `${names[0].charAt(0)}.${names[1].charAt(0)}`
  }

  //getalllessonhour - - trzeba zrobic

}

