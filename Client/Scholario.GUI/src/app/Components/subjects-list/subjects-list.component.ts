import { Component, inject } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { Subject } from '../../Type/Subject';
import { CommonModule } from '@angular/common';
import { SubjectComponent } from "../subject/subject.component";


@Component({
  selector: 'app-subjects-list',
  imports: [CommonModule, SubjectComponent],
  templateUrl: './subjects-list.component.html',
  styleUrl: './subjects-list.component.scss'
})
export class SubjectsListComponent {
  subjects: Subject[] = [];
  private readonly apiService = inject(ApiService);
  
  ngOnInit(){
    this.getSubject();
  }
  getSubject() {
    this.apiService.getSubjects().subscribe(
      response => {
        this.subjects = response.subjects;
      },
      error => console.error('Błąd podczas pobierania przedmiotów:', error)
    );
  }
}
