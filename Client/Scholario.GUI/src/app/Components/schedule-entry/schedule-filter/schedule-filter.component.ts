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
  selectedGroupId: number | null = null;
  @Output() onFilterChange = new EventEmitter<number>();

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

  onGroupChange() {
    this.onFilterChange.emit(this.selectedGroupId!);
  }

  clearFilters() {
    this.selectedGroupId = null;
    this.onFilterChange.emit(this.selectedGroupId!);
  }
}
