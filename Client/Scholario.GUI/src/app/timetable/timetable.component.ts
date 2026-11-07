
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DragDropModule, CdkDragDrop } from '@angular/cdk/drag-drop';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

import { ScheduleEntryService } from '../../Service/schedule-entry.service';
import { SubjectService } from '../../Service/subject.service';
import { GroupService } from '../../Service/group.service';
import { ClassroomService } from '../../Service/classroom.service';

import { Classroom } from '../Type/Classroom';
import { LessonHour } from '../Type/LessonHour';
import { Subject } from '../Type/Subject';
import { ScheduleEntry } from '../Type/ScheduleEntry';
import { Group } from '../Type/Group';
import { DayOfWeek } from '../Type/DayOfWeek';

import { ClassroomDialogComponent } from './classroom-dialog/classroom-dialog.component';

@Component({
  selector: 'app-timetable',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    DragDropModule,
    MatDialogModule
  ],
  templateUrl: './timetable.component.html',
  styleUrl: './timetable.component.scss'
})
export class TimetableComponent implements OnInit {

  private dialog = inject(MatDialog);

  days = [
    'Poniedziałek',
    'Wtorek',
    'Środa',
    'Czwartek',
    'Piątek'
  ];

  lessonHours: LessonHour[] = [];
  selectedGroupId = 1;

  subjects: Subject[] = [];
  scheduleEntries: ScheduleEntry[] = [];

  // Wszystkie wpisy ze wszystkich grup - do kontroli zajętości sal.
  allScheduleEntries: ScheduleEntry[] = [];

  classrooms: Classroom[] = [];
  readonly emptyDropList: Subject[] = [];

  dayDropLists: string[] = [];
  groups: Group[] = [];

  constructor(
    private scheduleService: ScheduleEntryService,
    private subjectService: SubjectService,
    private groupService: GroupService,
    private classroomService: ClassroomService
  ) {}

  ngOnInit(): void {
    this.loadData();
    this.getAllGroups();
    this.loadClassrooms();
    this.loadAllScheduleEntries();

    this.onGroupChange();
  }

  generateDropListIds(): void {
    this.dayDropLists = [];

    for (let d = 0; d < this.days.length; d++) {
      for (let h = 0; h < this.lessonHours.length; h++) {
        this.dayDropLists.push(
          `dropList-${d}-${this.lessonHours[h].lessonNumber}`
        );
      }
    }
  }

  loadData(): void {
    this.scheduleService.getAllLessonHours().subscribe({
      next: hours => {
        this.lessonHours = hours;
        this.generateDropListIds();
      },
      error: err => console.error('Błąd pobierania godzin:', err)
    });
  }

  loadClassrooms(): void {
    this.classroomService.getAllClassrooms().subscribe({
      next: classrooms => {
        this.classrooms = classrooms;
      },
      error: err => {
        console.error('Błąd pobierania sal:', err);
        alert('Nie udało się pobrać listy sal.');
      }
    });
  }

  loadAllScheduleEntries(): void {
    this.scheduleService.getScheduleEntries().subscribe({
      next: entries => {
        this.allScheduleEntries = entries.map(e => ({
          ...e,
          day: this.normalizeDay(e.day)
        }));
      },
      error: err => console.error('Błąd pobierania wszystkich wpisów:', err)
    });
  }

  getAllGroups(): void {
    this.groupService.getAllGroups().subscribe({
      next: groups => this.groups = groups,
      error: err => console.error(err)
    });
  }

  onGroupChange(): void {
    this.scheduleService
      .getSubjectsByGroupId(Number(this.selectedGroupId))
      .subscribe({
        next: subjects => this.subjects = subjects,
        error: err => console.error('Błąd pobierania przedmiotów:', err)
      });

    this.scheduleService
      .getScheduleByGroupId(Number(this.selectedGroupId))
      .subscribe({
        next: entries => {
          this.scheduleEntries = entries.map(e => ({
            ...e,
            day: this.normalizeDay(e.day)
          }));
        },
        error: err => console.error('Błąd pobierania planu:', err)
      });
  }

  private normalizeDay(day: any): DayOfWeek {
    if (typeof day === 'number') {
      return day as DayOfWeek;
    }

    if (typeof day === 'string' && day in DayOfWeek) {
      return DayOfWeek[day as keyof typeof DayOfWeek] as DayOfWeek;
    }

    return Number(day) as DayOfWeek;
  }

  drop(
    event: CdkDragDrop<Subject[]>,
    dayIndex: number,
    lessonNumber: number
  ): void {

    if (event.previousContainer === event.container) {
      return;
    }

    const subject = event.previousContainer.data[event.previousIndex];

    if (!subject) {
      return;
    }

    const existingIndex = this.scheduleEntries.findIndex(
      e => Number(e.day) === dayIndex &&
           e.lessonNumber === lessonNumber
    );

    // Zachowujemy dotychczasową możliwość zastąpienia wpisu w komórce.
    const occupiedClassroomIds = new Set(
      this.allScheduleEntries
        .filter(e =>
          Number(e.day) === dayIndex &&
          e.lessonNumber === lessonNumber &&
          e.classroomNumber != null &&
          e.groupId !== this.selectedGroupId
        )
        .map(e => Number(e.classroomNumber))
    );

    // Uwzględniamy także niezapisane wpisy w aktualnym planie.
    this.scheduleEntries
      .filter(e =>
        Number(e.day) === dayIndex &&
        e.lessonNumber === lessonNumber &&
        e.classroomNumber != null &&
        e !== this.scheduleEntries[existingIndex]
      )
      .forEach(e => occupiedClassroomIds.add(Number(e.classroomNumber)));

    const availableClassrooms = this.classrooms.filter(
      classroom => !occupiedClassroomIds.has(classroom.id)
    );

    const dialogRef = this.dialog.open(ClassroomDialogComponent, {
      width: '450px',
      data: {
        subjectName: subject.name,
        dayName: this.days[dayIndex],
        lessonNumber,
        classrooms: availableClassrooms
      }
    });

    dialogRef.afterClosed().subscribe((classroomId: number | null | undefined) => {

      // undefined oznacza anulowanie.
      if (classroomId === undefined) {
        return;
      }

      const newEntry: ScheduleEntry = {
        subjectId: subject.id,
        subjectName: subject.name,
        groupId: Number(this.selectedGroupId),
        day: dayIndex as DayOfWeek,
        lessonNumber,
        classroomNumber: classroomId ?? undefined,
        _isNew: true
      };

      const currentIndex = this.scheduleEntries.findIndex(
        e => Number(e.day) === dayIndex &&
             e.lessonNumber === lessonNumber
      );

      if (currentIndex >= 0) {
        this.scheduleEntries[currentIndex] = newEntry;
      } else {
        this.scheduleEntries.push(newEntry);
      }

      // Odświeżamy listę dla kontroli zajętości kolejnych lekcji.
      this.allScheduleEntries = [
        ...this.allScheduleEntries.filter(e =>
          !(e.groupId === this.selectedGroupId &&
            Number(e.day) === dayIndex &&
            e.lessonNumber === lessonNumber)
        ),
        newEntry
      ];
    });
  }

  saveSchedule(): void {
    const toSave = this.scheduleEntries.filter(e => e._isNew);

    if (toSave.length === 0) {
      alert('Brak nowych wpisów do zapisania!');
      return;
    }

    console.log('Przed zapisem saveSchedule:');
    console.table(toSave);

    this.scheduleService.createScheduleEntries(toSave).subscribe({
      next: () => {
        alert('Plan zapisany pomyślnie!');

        toSave.forEach(e => e._isNew = false);

        this.loadAllScheduleEntries();

        console.table(this.scheduleEntries);
      },
      error: err => {
        console.error(err);
        alert('Błąd przy zapisie planu!');
      }
    });
  }

  getSubjectName(day: number, lessonNumber: number): string {
    const entry = this.scheduleEntries.find(
      e => Number(e.day) === day &&
           e.lessonNumber === lessonNumber
    );

    return entry ? entry.subjectName : '';
  }

  getClassroomName(day: number, lessonNumber: number): string {
    const entry = this.scheduleEntries.find(
      e => Number(e.day) === day &&
           e.lessonNumber === lessonNumber
    );

    if (entry?.classroomNumber == null) {
      return '';
    }

    const classroom = this.classrooms.find(
      c => c.id === Number(entry.classroomNumber)
    );

    return classroom ? `Sala ${classroom.number}` : '';
  }

  getInitials(subject: Subject): string {
    if (!subject.teacherName) {
      return '';
    }

    const names = subject.teacherName.trim().split(/\s+/);

    return names.length >= 2
      ? `${names[0].charAt(0)}.${names[1].charAt(0)}`
      : names[0].charAt(0);
  }
}
