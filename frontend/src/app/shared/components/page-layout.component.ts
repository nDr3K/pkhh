import { Component } from '@angular/core';
import { NavBarComponent } from "./navigation/desktop";
import { MobileNavBarComponent } from "./navigation/mobile";

@Component({
  standalone: true,
  imports: [NavBarComponent, MobileNavBarComponent],
  selector: 'app-page-layout',
  template: `
    <div class="page-layout">
      <app-nav-bar></app-nav-bar>
      <app-mobile-nav-bar></app-mobile-nav-bar>
      <div class="page-layout__content">
        <ng-content></ng-content>
      </div>
<!--      <app-footer></app-footer>-->
    </div>
  `,
})
export class PageLayoutComponent {}
