import { Component, Input } from '@angular/core';
import { Subject } from '../../Type/Subject';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-subject',
  imports: [CommonModule, RouterModule],
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.scss'
})
export class SubjectComponent {
  @Input() subject!: Subject;
  isSelected: boolean = false;
  getSubjectInfo(){
    this.isSelected = true
    console.log("sss")
  }
}
