import { Component, EventEmitter, Output } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgOptimizedImage } from "@angular/common";

@Component({
  standalone: true,
  imports: [RouterModule, NgOptimizedImage],
  selector: 'app-mobile-nav-bar-brand',
  template: `
    <div class="mobile-nav-bar__brand">
      <a (click)="onMobileNavBarBrandClick()" routerLink="/">
        <img
          class="mobile-nav-bar__logo"
          ngSrc="https://cdn.andreacannavo.com/pkhh/logo.png"
          alt="pkhh app logo"
          width="32"
          height="32"
        />
      </a>
    </div>
  `,
})
export class MobileNavBarBrandComponent {
  @Output() mobileNavBarBrandClick = new EventEmitter<void>();

  onMobileNavBarBrandClick(): void {
    this.mobileNavBarBrandClick.emit();
  }
}
