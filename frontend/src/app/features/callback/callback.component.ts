import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from "@auth0/auth0-angular";
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { Router } from "@angular/router";
import { catchError, filter, of, take } from "rxjs";
import { UserService } from "../../core/services/user.service";
import { UserStateService } from "../../core/services/user-state.service";
import { UserProfile } from "../../core/models/user-profile";

@Component({
  selector: 'app-callback',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent],
  templateUrl: './callback.component.html',
})
export class CallbackComponent implements OnInit {
  private auth = inject(AuthService);
  private userService = inject(UserService);
  private userStateService = inject(UserStateService);
  private router = inject(Router);

  error$ = this.auth.error$;

  isProcessingBackend = false;
  backendError: string | null = null;

  ngOnInit(): void {
    // Wait for Auth0 to finish processing
    this.auth.isLoading$
      .pipe(
        filter(loading => !loading), // Wait until Auth0 loading is complete
        take(1)
      )
      .subscribe(() => {
        this.handleAuthCallback();
      });
  }

  private handleAuthCallback(): void {
    this.auth.isAuthenticated$.pipe(take(1)).subscribe(isAuthenticated => {
      if (isAuthenticated) {
        this.authenticateWithBackend();
      } else {
        // Check for Auth0 errors
        this.auth.error$.pipe(take(1)).subscribe(error => {
          if (error) {
            console.error('Auth0 error:', error);
          } else {
            // No error but not authenticated - redirect to home
            this.router.navigate(['/']).then();
          }
        });
      }
    });
  }

  private authenticateWithBackend(): void {
    this.isProcessingBackend = true;
    this.backendError = null;

    this.userService.authenticateWithBackend()
      .pipe(
        catchError(error => {
          console.error('Backend authentication error:', error);
          this.backendError = 'Failed to connect to our servers. Please try again.';
          this.isProcessingBackend = false;
          return of(null);
        })
      )
      .subscribe((response: UserProfile | null) => {
        this.isProcessingBackend = false;
        if (response) {
          this.handleSuccessfulAuth(response);
        }
      });
  }

  private handleSuccessfulAuth(user: UserProfile): void {
    // Store user data in state management
    this.userStateService.setUser(user);
    this.router.navigate(['/dashboard']).then();
  }
}
