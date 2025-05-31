import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgOptimizedImage } from "@angular/common";

@Component({
  standalone: true,
  imports: [RouterModule, NgOptimizedImage],
  selector: 'app-nav-bar-brand',
  template: `
    <div class="nav-bar__brand">
      <a routerLink="/">
        <img
          class="nav-bar__logo"
          ngSrc="https://cdn.andreacannavo.com/pkhh/logo.png"
          alt="pkhh app logo"
          width="56"
          height="56"
        />
      </a>
    </div>
  `,
})
export class NavBarBrandComponent {}
