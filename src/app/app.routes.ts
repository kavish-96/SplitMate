import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { GroupDetailComponent } from './components/group-detail/group-detail.component';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'group/:id', component: GroupDetailComponent },
  { path: '**', redirectTo: '' }
];