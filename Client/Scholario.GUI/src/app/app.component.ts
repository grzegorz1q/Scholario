import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { NgFor } from "@angular/common";
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from "./login/login.component";
import { ScheduleEntryComponent } from './schedule-entry/schedule-entry.component';

@Component({
  selector: 'app-root',
  imports: [CommonModule , RouterOutlet, LoginComponent, ScheduleEntryComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Scholario.GUI';
}
