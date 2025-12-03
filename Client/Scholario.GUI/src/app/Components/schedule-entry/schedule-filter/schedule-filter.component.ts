import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Group } from '../../../Type/Group';
import { GroupService } from '../../../../Service/group.service';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-schedule-filter',
  imports: [FormsModule, NgFor],
  templateUrl: './schedule-filter.component.html',
  styleUrl: './schedule-filter.component.scss'
})
export class ScheduleFilterComponent implements OnInit {
  availableGroups: Group[] = [];
  filters = {
    groupId: null,
    teacherId: null,
    classroomId: null
  }
  @Output() filtersChanged = new EventEmitter<any>();
  constructor(private groupService: GroupService) {}

  ngOnInit() {
    this.getAllGroups();
  }

  getAllGroups() {
    this.groupService.getAllGroups().subscribe({
      next: groups => {
        this.availableGroups = groups;
        console.table(this.availableGroups);
      },
      error: error => console.error(error)
    });
  }

  onFiltersChange() {
    this.filtersChanged.emit(this.filters);
  }

  clearFilters() {
    this.filters.groupId = null;
    this.filters.teacherId = null;
    this.filters.classroomId = null;
    this.filtersChanged.emit(this.filters);
  }
}
