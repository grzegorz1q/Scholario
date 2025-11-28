import { Component, inject } from '@angular/core';
import { Student } from '../../Type/Student';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Grade } from '../../Type/Grade';
import { HttpErrorResponse } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { GradeWeight } from '../../Type/GradeWeight';
import { GradeService } from '../../../Service/grade.service';
import { SubjectService } from '../../../Service/subject.service';

@Component({
  selector: 'app-subject-group',
  imports: [CommonModule, FormsModule],
  templateUrl: './subject-group.component.html',
  styleUrl: './subject-group.component.scss'
})
export class SubjectGroupComponent {
  subjectId!: number;
  groupId!: number;
  students: Student[] = [];
  aditionalInformation = false;
  grade: Grade | null = null;
  showGradeForm = false;
  selectedStudent: Student | null = null;
  newGrade!: Grade;
  gradeWeightOptions = Object.entries(GradeWeight).map(([name, value]) => ({
    name: name.replace(/([a-z])([A-Z])/g, '$1 $2'),
    value
  }));

  readView = true;

  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly gradeService = inject(GradeService);
  private readonly subjectService = inject(SubjectService);

  ngOnInit(): void {
    this.subjectId = +this.route.snapshot.paramMap.get('subjectId')!;
    this.groupId = +this.route.snapshot.paramMap.get('groupId')!;  

    this.getStudentsBySubjectGroup();
  }
  openGradeForm(student: any) {
    this.selectedStudent = student;
    this.newGrade = {
      gradeValue: 0,
      gradeWeight: 1,
      subjectName: '',
      studentId: student.id,
      subjectId: this.subjectId,
      descriptiveAssessmentId: 0
    };
    this.showGradeForm = true;
  }
  addGrade(event: Event){
    event.preventDefault();
    if (this.selectedStudent && this.newGrade.gradeValue) {
      this.newGrade.gradeWeight = Number(this.newGrade.gradeWeight);
      this.gradeService.addGrade(this.newGrade).subscribe({
        next: () => {
          this.getStudentsBySubjectGroup();
          this.showGradeForm = false;
        },
        error: (error) => console.error('Błąd przy dodawaniu oceny:', error)
      });
    }
  }
  updateGrade(grade: Grade){
    grade.gradeWeight = Number(grade.gradeWeight);
    this.gradeService.updateGrade(grade).subscribe({
      next: response => {
        console.log(grade.gradeWeight);
        this.getStudentsBySubjectGroup(); //works but I think that it's not good practice. 
                                          // Probably better to add GetGradeById method
        this.readView = true;
      },
      error: error => console.error(error)
    });
  }
  getStudentsBySubjectGroup(){
    this.subjectService.getStudentsBySubjectGroup(this.subjectId, this.groupId).subscribe(
      (students) => {
        this.students = students;
      },
      (error: HttpErrorResponse) => {
        this.router.navigate(['/subjects']);
        console.log("blad")
        if(error.status == 403){
          alert('Nie masz dostępu do tych danych');
        }else if (error.status === 404) {
          alert('Nie znaleziono przedmiotu lub grupy.');
        }else {
          alert('Wystąpił błąd. Spróbuj ponownie później.');
        }
      }
    );
  }
  getAditionalInformation(){
    this.readView = true;
    this.aditionalInformation = true;
  }
}
