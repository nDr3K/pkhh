import { Component } from '@angular/core';
import { LoadingService } from "../../core/services/loading.service";
import { AsyncPipe, NgIf } from "@angular/common";

@Component({
  standalone: true,
  selector: 'app-page-loader',
  template: `
    <div *ngIf="loadingService.loading$ | async" class="loader-overlay">
      <div class="loader"></div>
    </div>
  `,
  imports: [
    NgIf,
    AsyncPipe
  ]
})
export class PageLoaderComponent {
  constructor(public loadingService: LoadingService) {}
}
