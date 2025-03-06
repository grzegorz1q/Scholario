import { Component, inject, Input } from '@angular/core';
import { Subject } from '../../Type/Subject';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../Service/authService';

@Component({
  selector: 'app-subject',
  imports: [CommonModule, RouterModule],
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.scss'
})
export class SubjectComponent {
  @Input() subject!: Subject;
  isSelected: boolean = false;
  isTeacher = false;
  private readonly authService = inject(AuthService);
  ngOnInit(){
    this.isTeacher = this.authService.isTeacher();
  }
  getSubjectInfo(){
    this.isSelected = true
  }
}
