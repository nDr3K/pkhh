import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AuthService } from "@auth0/auth0-angular";
import { environment } from "../../../environments/environment";
import { catchError, Observable, switchMap } from "rxjs";
import { GameSave } from "../models/game-save";
import { PagedResponse } from "../models/paged-response";
import { SaveFile } from "../models/save-file";

@Injectable({
  providedIn: 'root'
})
export class SaveService {
  private http = inject(HttpClient);
  private auth = inject(AuthService);
  private apiUrl = environment.api.serverUrl;

  getSaves(pageNumber: number = 1, pageSize: number = 10): Observable<PagedResponse<GameSave>> {
    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        });

        return this.http.get<PagedResponse<GameSave>>(
          `${this.apiUrl}/saves?pageNumber=${pageNumber}&pageSize=${pageSize}`,
          { headers }
        );
      }),
      catchError(error => {
        console.error('Failed to retrieve saves:', error);
        throw error;
      })
    );
  }

  uploadSaveFile(metadata: SaveFile, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('SaveFile', file);
    formData.append('Metadata', JSON.stringify(metadata));

    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`
        });

        return this.http.post<any>(`${this.apiUrl}/saves`, formData, { headers });
      }),
      catchError(error => {
        console.error('Failed to retrieve saves:', error);
        throw error;
      })
    );
  }
}
