import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubjectComponent } from "../subject/subject.component";
import { LoggedUserSubjects } from '../../Type/LoggedUserSubjects';
import { SubjectService } from '../../../Service/subject.service';


@Component({
  selector: 'app-subjects-list',
  imports: [CommonModule, SubjectComponent],
  templateUrl: './subjects-list.component.html',
  styleUrl: './subjects-list.component.scss'
})
export class SubjectsListComponent {

  subjects: LoggedUserSubjects | undefined = undefined;
  private readonly subjectService = inject(SubjectService)

  ngOnInit() {
    this.getSubjects();
  }

  getSubjects() {
    this.subjectService.getLoggedUserSubjects().subscribe({
      next: response => {
        this.subjects = response;
      },
      error: error => console.error('Błąd podczas pobierania przedmiotów:', error)
    });
  }
}
