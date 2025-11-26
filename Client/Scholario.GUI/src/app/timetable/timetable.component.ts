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
import { DayOfWeek } from '../Type/DayOfWeek';
import { Classroom } from '../Type/Classroom';
import { ClassroomService } from '../../Service/classroom.service';

@Component({
  selector: 'app-timetable',
  imports: [DragDropModule, CommonModule, FormsModule],
  templateUrl: './timetable.component.html',
  styleUrl: './timetable.component.scss'
})

export class TimetableComponent implements OnInit {
  days = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek'];
  lessonHours: LessonHour[] = [];
  selectedGroupId: number = 1;
  subjects: Subject[] = [];
  scheduleEntries: ScheduleEntry[] = [];
  dayDropLists: string[] = [];
  groups: Group[] = [];
  initialsOfName: string[] = [];
  classrooms: Classroom[] = [];

  constructor(private scheduleService: ScheduleEntryService, private subjectService: SubjectService, private groupService: GroupService, private classroomService: ClassroomService) { }

  ngOnInit() {
    console.log("Metoda ngOnInit");
    this.selectedGroupId = 1;
    this.loadData();
    this.getAllGroups();
    this.groupSubject();
    this.onGroupChange();
    this.getClassrooms();
  }

  getClassrooms(){
    this.classroomService.getClassrooms().subscribe({
      next: (classrooms) =>
        this.classrooms = classrooms,
      error: (err) => {
        console.error(err)
      }
    });
  }

  generateDropListIds() {
    this.dayDropLists = [];
    for (let d = 0; d < this.days.length; d++) {
      for (let h = 0; h < this.lessonHours.length; h++) {
        this.dayDropLists.push(`dropList-${d}-${h + 1}`);
      }
    }
  }

  groupSubject() {
    this.scheduleService.getScheduleByGroupId(this.selectedGroupId).subscribe(entries => {
      this.scheduleEntries = entries.filter(e => e.groupId === this.selectedGroupId);
    });
    this.generateDropListIds();
  }

  loadData() {
    this.scheduleService.getAllLessonHours().subscribe(hours => {
      this.lessonHours = hours;
      this.generateDropListIds();
    });
  }

  onGroupChange() {
    this.scheduleService.getSubjectsByGroupId(this.selectedGroupId).subscribe(r => this.subjects = r);
    this.scheduleService.getScheduleByGroupId(this.selectedGroupId)
      .subscribe(entries => {
        this.scheduleEntries = entries.map(e => ({
          ...e,
          day: DayOfWeek[e.day as unknown as keyof typeof DayOfWeek] as DayOfWeek
        }));
      });
  }

  getAllGroups() {
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
    if (event.previousContainer === event.container) return;

    const subject = event.previousContainer.data[event.previousIndex];

    const newEntry: ScheduleEntry = {
      subjectId: subject.id,
      subjectName: subject.name,
      groupId: Number(this.selectedGroupId),
      day: dayIndex,
      lessonNumber: lessonNumber,
      _isNew: true
    };

    const existingIndex = this.scheduleEntries.findIndex(
      e => e.day === dayIndex && e.lessonNumber === lessonNumber
    );

    if (existingIndex >= 0) {
      this.scheduleEntries[existingIndex] = newEntry;
    } else {
      this.scheduleEntries.push(newEntry);
    }
  }

saveSchedule() {
  const toSave = this.scheduleEntries.filter(e => e._isNew);

  if (toSave.length === 0) {
    alert("Brak nowych wpisów do zapisania!");
    return;
  }

  console.log("Przed zapisem saveSchedule:");
  console.table(toSave);

  this.scheduleService.createScheduleEntries(toSave).subscribe({
    next: () => {
      alert("Plan zapisany pomyślnie!");
      toSave.forEach(e => e._isNew = false);
      console.table(this.scheduleEntries);
    },
    error: (err) => {
      console.error(err);
      alert("Błąd przy zapisie planu!");
      console.table(this.scheduleEntries);
    }
  });
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
}

