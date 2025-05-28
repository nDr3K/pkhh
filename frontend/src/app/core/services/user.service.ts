import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { AuthService } from "@auth0/auth0-angular";
import { catchError, Observable, switchMap } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);
  private auth = inject(AuthService);
  private apiUrl = environment.api.serverUrl;

  authenticateWithBackend(): Observable<any> {
    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        });

        // Call your backend's auth endpoint
        return this.http.get<any>(`${this.apiUrl}/auth/login`, { headers });
      }),
      catchError(error => {
        console.error('Backend authentication failed:', error);
        throw error;
      })
    );
  }
}
