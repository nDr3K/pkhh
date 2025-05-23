import { Route } from '@angular/router';

export const ROUTES: Route[] = [
  { path: '', pathMatch: 'full', loadComponent: () =>
      import('./features/home/home.component').then(m => m.HomeComponent) },
];
