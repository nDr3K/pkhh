import { Component } from '@angular/core';
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { NgOptimizedImage } from "@angular/common";

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  imports: [
    PageLayoutComponent,
    NgOptimizedImage
  ],
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

}
