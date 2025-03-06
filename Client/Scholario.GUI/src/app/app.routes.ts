import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component'; 
import { ScheduleEntryComponent } from './Components/schedule-entry/schedule-entry.component';
import { AuthGuard } from '../Service/auth.guard';
import { GradeComponent } from './Components/grade-component/grade-component.component';
import { SubjectsListComponent } from './Components/subjects-list/subjects-list.component';
import { SubjectGroupComponent } from './Components/subject-group/subject-group.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' }, 
  { path: 'login', component: LoginComponent }, 
  { path: 'schedule', component: ScheduleEntryComponent, canActivate: [AuthGuard] }, 
  { path: 'grade', component: GradeComponent, canActivate: [AuthGuard] }, 
  { path: 'subjects', component: SubjectsListComponent, canActivate: [AuthGuard]},
  { path: 'subjects/:subjectId/groups/:groupId', component: SubjectGroupComponent, canActivate: [AuthGuard]},

];
