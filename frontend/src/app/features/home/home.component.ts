import { Component } from '@angular/core';
import { PageLayoutComponent } from "../../shared/components/page-layout.component";

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  imports: [
    PageLayoutComponent
  ],
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

}
