
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef
} from '@angular/material/dialog';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';

import { Classroom } from '../../Type/Classroom';

export interface ClassroomDialogData {
  subjectName: string;
  dayName: string;
  lessonNumber: number;
  classrooms: Classroom[];
}

@Component({
  selector: 'app-classroom-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './classroom-dialog.component.html',
  styleUrl: './classroom-dialog.component.scss'
})
export class ClassroomDialogComponent {
  private dialogRef =
    inject(MatDialogRef<ClassroomDialogComponent>);

  data = inject<ClassroomDialogData>(MAT_DIALOG_DATA);

  selectedClassroomId: number | null = null;

  confirm(): void {
    this.dialogRef.close(this.selectedClassroomId);
  }

  cancel(): void {
    this.dialogRef.close(undefined);
  }
}
