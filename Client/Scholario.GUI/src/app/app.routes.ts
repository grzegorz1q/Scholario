import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component'; 
import { ScheduleEntryComponent } from './schedule-entry/schedule-entry.component';
import { AuthGuard } from '../Service/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, 
  { path: 'login', component: LoginComponent }, 
  { path: 'schedule', component: ScheduleEntryComponent, canActivate: [AuthGuard] }, 
];
