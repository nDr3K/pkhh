import { Route } from '@angular/router';
import { AuthGuard } from "@auth0/auth0-angular";
import { emulatorGuard } from "./features/emulator/guard/emulator.guard";

export const ROUTES: Route[] = [
  { path: '', pathMatch: 'full', loadComponent: () =>
      import('./features/home/home.component').then(m => m.HomeComponent) },
  { path: 'callback', loadComponent: () =>
      import('./features/callback/callback.component').then(m => m.CallbackComponent) },
  { path: 'dashboard', loadComponent: () =>
      import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent), canActivate: [AuthGuard] },
  { path: 'detail/:id', loadComponent: () =>
      import('./features/detail/detail.component').then(m => m.DetailComponent), canActivate: [AuthGuard] },
  { path: 'roms', loadComponent: () =>
      import('./features/roms/roms.component').then(m => m.RomsComponent), canActivate: [AuthGuard] },
  { path: 'emulator', loadComponent: () =>
      import('./features/emulator/emulator.component').then(m => m.EmulatorComponent), canActivate: [AuthGuard], canDeactivate: [emulatorGuard] }
];
