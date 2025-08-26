// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ForecastComponent } from './features/forecast/forecast.component';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'forecast/:cityId', component: ForecastComponent },
];
