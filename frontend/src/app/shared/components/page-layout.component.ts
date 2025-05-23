import { Component } from '@angular/core';
import { NavBarComponent } from "./navigation/desktop";

@Component({
  standalone: true,
  imports: [NavBarComponent],
  selector: 'app-page-layout',
  template: `
    <div class="page-layout">
      <app-nav-bar></app-nav-bar>
      <div class="page-layout__content">
        <ng-content></ng-content>
      </div>
<!--      <app-footer></app-footer>-->
    </div>
  `,
})
export class PageLayoutComponent {}
