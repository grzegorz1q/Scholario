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

  getWeightedAverage(grades: { gradeValue: number, gradeWeightValue: number }[]): number {
    if (!grades || grades.length === 0) return 0;

    let totalWeight = 0;
    let weightedSum = 0;

    grades.forEach(g => {
        weightedSum += g.gradeValue * g.gradeWeightValue;
        totalWeight += g.gradeWeightValue;
    });

    return totalWeight > 0 ? parseFloat((weightedSum / totalWeight).toFixed(2)) : 0;
}

  getAditionalInformation(){
    this.aditionalInformation = true;
  }
}