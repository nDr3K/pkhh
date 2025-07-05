import { catchError, Observable, switchMap, throwError } from "rxjs";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthService } from "@auth0/auth0-angular";

export abstract class BaseService {
  protected http = inject(HttpClient);
  protected auth = inject(AuthService);

  makeAuthenticatedRequest<T>(
    method: 'GET' | 'POST' | 'PUT' | 'DELETE',
    url: string,
    options: { body?: any; params?: any } = {}
  ): Observable<T> {
    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const isFormData = options.body instanceof FormData;

        let headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`,
        });

        // Only set Content-Type if not FormData
        if (!isFormData) {
          headers = headers.set('Content-Type', 'application/json');
        }

        const httpOptions = {
          headers,
          params: options.params
        };

        switch (method) {
          case 'GET':
            return this.http.get<T>(url, httpOptions);
          case 'POST':
            return this.http.post<T>(url, options.body, httpOptions);
          case 'PUT':
            return this.http.put<T>(url, options.body, httpOptions);
          case 'DELETE':
            return this.http.delete<T>(url, httpOptions);
          default:
            throw new Error(`Unsupported HTTP method: ${method}`);
        }
      }),
      catchError(error => {
        console.error('Authenticated request failed:', error);
        return throwError(() => error);
      })
    );
  }
}
