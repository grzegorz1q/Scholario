import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SubjectCreateDto } from '../../Type/SubjectCreateDto';
import { Subject } from '../../Type/Subject';
import { SubjectService } from '../../../Service/subject.service';

@Component({
  selector: 'app-subject-management',
  imports: [CommonModule, FormsModule],
  templateUrl: './subject-management.component.html',
  styleUrl: './subject-management.component.scss'
})
export class SubjectManagementComponent {
      subjects: Subject[] = [];
      teachers: number = 0;

  newSubject: SubjectCreateDto = {
    name: '',
    description: '',
    teacherId: 0
  };

  constructor(private subjectService: SubjectService ){}

  createSubject(){
    if (!this.newSubject.name || !this.newSubject.teacherId) {
      alert('Wypełnij wymagane pola');
      return;
    }

this.subjectService.createSubject(this.newSubject).subscribe({
      next: (created: Subject) => {
        alert('Przedmiot utworzony!');
        this.subjects.push(created); 
        this.newSubject = { name: '', description: '', teacherId: 0 }; 
      },
      error: (err) => {
        console.error('Błąd podczas tworzenia przedmiotu', err);
        alert('Wystąpił błąd podczas tworzenia przedmiotu.');
      }
    });
  }
}
