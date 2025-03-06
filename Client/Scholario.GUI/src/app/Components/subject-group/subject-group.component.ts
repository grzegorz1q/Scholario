import { Component, inject } from '@angular/core';
import { Student } from '../../Type/Student';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../../Service/api.service';
import { CommonModule } from '@angular/common';

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
  
  private readonly route = inject(ActivatedRoute);
  private readonly apiService = inject(ApiService);

  ngOnInit(): void {
    this.subjectId = +this.route.snapshot.paramMap.get('subjectId')!;
    this.groupId = +this.route.snapshot.paramMap.get('groupId')!;
    console.log("Subject ID:", this.subjectId);
    console.log("Group ID:", this.groupId);

    // Po uzyskaniu ID, pobieramy listę studentów
    this.apiService.getStudentsBySubjectGroup(this.subjectId, this.groupId).subscribe(
      (students) => {
        this.students = students;
        console.log(this.students);
      },
      (error) => console.error('Błąd podczas pobierania studentów:', error)
    );
  }
}
