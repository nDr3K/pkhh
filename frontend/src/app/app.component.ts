import { Component, inject } from '@angular/core';
import { RouterModule } from "@angular/router";
import { PageLoaderComponent } from "./shared/components/page-loader.component";
import { AuthService } from "@auth0/auth0-angular";
import { CommonModule } from "@angular/common";

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  imports: [CommonModule, RouterModule, PageLoaderComponent]
})
export class AppComponent {
  private auth = inject(AuthService);
  isAuth0Loading$ = this.auth.isLoading$;
}
