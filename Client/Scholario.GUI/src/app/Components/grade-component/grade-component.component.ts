import { StudentGrade } from './../../Type/StudentGrade';
import { Subject } from './../../Type/Subject';
import { ApiService } from './../../../Service/api.service';
import { Component, inject } from '@angular/core';
import { Grade } from '../../Type/Grade';
import { CommonModule, NgFor } from '@angular/common';

@Component({
  selector: 'app-grade-component',
  imports: [CommonModule],
  templateUrl: './grade-component.component.html',
  styleUrl: './grade-component.component.scss'
})

export class GradeComponent {
  private readonly apiService = inject(ApiService)
  studentGrades: StudentGrade[] = [];

  ngOnInit() {
    this.getGrades()
  }

  getGrades() {
    this.apiService.getGradeByStudent().subscribe(
      response => {
        console.log(response)
        this.studentGrades = response;
      },
      error => {
        console.error('Błąd podczas pobierania ocen', error);
      }
    );
  }


}