import { Component, inject } from '@angular/core';
import { Student } from '../../Type/Student';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../../Service/api.service';
import { CommonModule } from '@angular/common';
import { Grade } from '../../Type/Grade';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../../../Service/authService';

@Component({
  selector: 'app-subject-group',
  imports: [CommonModule],
  templateUrl: './subject-group.component.html',
  styleUrl: './subject-group.component.scss'
})
export class SubjectGroupComponent {
  subjectId!: number;
  groupId!: number;
  students: Student[] = [];
  aditionalInformation = false;
  grade: Grade | null = null;
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly apiService = inject(ApiService);

  ngOnInit(): void {
    this.subjectId = +this.route.snapshot.paramMap.get('subjectId')!;
    this.groupId = +this.route.snapshot.paramMap.get('groupId')!;  

    this.apiService.getStudentsBySubjectGroup(this.subjectId, this.groupId).subscribe(
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
    this.aditionalInformation = true;
  }
}
