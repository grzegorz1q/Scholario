import { StudentGrade } from './../../Type/StudentGrade';
import { ApiService } from './../../../Service/api.service';
import { Component, inject } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { Grade } from '../../Type/Grade';

@Component({
  selector: 'app-grade-component',
  imports: [CommonModule],
  templateUrl: './grade-component.component.html',
  styleUrl: './grade-component.component.scss'
})

export class GradeComponent {
  private readonly apiService = inject(ApiService)

  studentGrades: StudentGrade[] = [];
  grade: Grade | null = null;
  aditionalInformation = false;

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

  getAditionalInformation(){
    this.aditionalInformation = true;
  }
}