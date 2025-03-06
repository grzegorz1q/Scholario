import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component'; 
import { ScheduleEntryComponent } from './Components/schedule-entry/schedule-entry.component';
import { AuthGuard } from '../Service/auth.guard';
import { GradeComponent } from './Components/grade-component/grade-component.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, 
  { path: 'login', component: LoginComponent }, 
  { path: 'schedule', component: ScheduleEntryComponent, canActivate: [AuthGuard] }, 
  { path: 'grade', component: GradeComponent, canActivate: [AuthGuard] }, 

];
