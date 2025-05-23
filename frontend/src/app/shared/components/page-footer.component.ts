import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { PageFooterHyperlinkComponent } from './page-footer-hyperlink.component';

@Component({
  standalone: true,
  imports: [CommonModule, PageFooterHyperlinkComponent],
  selector: 'app-footer',
  template: `
    <footer class="page-footer">
      <div class="page-footer-message">
        Pokémon is © of Nintendo, 1995-2025
      </div>
      <div class="page-footer-info">
        <div
          *ngFor="let resource of resourceList"
          class="page-footer-info__item"
        >
          <app-page-footer-hyperlink [path]="resource.path">
            {{ resource.label }}
          </app-page-footer-hyperlink>
        </div>
      </div>
    </footer>

  `,
})
export class PageFooterComponent {
  resourceList: {path:string, label:string}[] = [
    {
      path: 'https://github.com/nDr3K',
      label: 'Made by Andrea Cannavò',
    },
    {
      path: '/about#privacy',
      label: 'Privacy Policy',
    },
  ];
}
