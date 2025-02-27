import { Component, inject } from '@angular/core';
import { ApiService } from '../../Service/api.service';

@Component({
  selector: 'app-schedule-entry',
  imports: [],
  templateUrl: './schedule-entry.component.html',
  styleUrl: './schedule-entry.component.scss'
})
export class ScheduleEntryComponent {
    private readonly schedyleEntryService = inject(ApiService)
    ngOnInit(){
      this.schedyleEntryService.getUsers()
    }
}
