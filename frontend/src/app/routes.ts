import { Route } from '@angular/router';

export const ROUTES: Route[] = [
  { path: '', pathMatch: 'full', loadComponent: () =>
      import('./features/home/home.component').then(m => m.HomeComponent) },
  {path: 'callback', loadComponent: () =>
      import('./features/callback/callback.component').then(m => m.CallbackComponent)}
];
