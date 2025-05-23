import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-page-loader',
  template: `
    <div class="loader-overlay">
      <div class="loader"></div>
    </div>
  `,
})
export class PageLoaderComponent {}
