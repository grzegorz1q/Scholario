import { Component, inject } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { Subject } from '../../Type/Subject';
import { CommonModule } from '@angular/common';
import { SubjectComponent } from "../subject/subject.component";
import { ScheduleEntryService } from '../../../Service/schedule-entry.service';
import { LoggedUserSubjects } from '../../Type/LoggedUserSubjects';
import { SubjectService } from '../../../Service/subject.service';


@Component({
  selector: 'app-subjects-list',
  imports: [CommonModule, SubjectComponent],
  templateUrl: './subjects-list.component.html',
  styleUrl: './subjects-list.component.scss'
})
export class SubjectsListComponent {

  subjects: Subject[] = [];
  private readonly subjectService = inject(SubjectService)

  ngOnInit() {
    this.getSubjects();
  }

  getSubjects() {
    this.subjectService.getSubjects().subscribe({
      next: response => {
        this.subjects = response.subjects;
      },
      error: error => console.error('Błąd podczas pobierania przedmiotów:', error)
    });
  }
}
