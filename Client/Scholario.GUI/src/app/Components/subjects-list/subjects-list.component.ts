import { Component, inject } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { Subject } from '../../Type/Subject';
import { CommonModule } from '@angular/common';
import { SubjectComponent } from "../subject/subject.component";
import { LoggedUserSubjects } from '../../Type/LoggedUserSubjects';


@Component({
  selector: 'app-subjects-list',
  imports: [CommonModule, SubjectComponent],
  templateUrl: './subjects-list.component.html',
  styleUrl: './subjects-list.component.scss'
})
export class SubjectsListComponent {
  subjects: LoggedUserSubjects | undefined;
  private readonly apiService = inject(ApiService);
  
  ngOnInit(){
    this.getSubjects();
  }
  getSubjects() {
    this.apiService.getLoggedUserSubjects().subscribe({
      next: response => {
        this.subjects = response;
      },
      error: error => console.error('Błąd podczas pobierania przedmiotów:', error)
  });
  }
}
