import { Component, inject } from '@angular/core';
import { ApiService } from '../../../Service/api.service';
import { Subject } from '../../Type/Subject';
import { CommonModule } from '@angular/common';
import { SubjectComponent } from "../subject/subject.component";
import { ScheduleEntryService } from '../../../Service/schedule-entry.service';


@Component({
  selector: 'app-subjects-list',
  imports: [CommonModule, SubjectComponent],
  templateUrl: './subjects-list.component.html',
  styleUrl: './subjects-list.component.scss'
})
export class SubjectsListComponent {
  subjects: Subject[] = [];
  private readonly scheduleEntryService = inject(ScheduleEntryService);
  
  ngOnInit(){
    this.getSubject();
  }
  getSubject() {
    this.scheduleEntryService.getSubjects().subscribe(
      response => {
        this.subjects = response.subjects;
      },
      error => console.error('Błąd podczas pobierania przedmiotów:', error)
    );
  }
}
