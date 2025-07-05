import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AuthService } from "@auth0/auth0-angular";
import { environment } from "../../../environments/environment";
import { catchError, Observable, switchMap } from "rxjs";
import { GameSave, GameSaveExtended } from "../models/game-save";
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

  getSaveFileById(id: string): Observable<GameSaveExtended> {
    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        });

        return this.http.get<GameSaveExtended>(
          `${this.apiUrl}/saves/${id}`,
          { headers }
        );
      }),
      catchError(error => {
        console.error('Failed to retrieve saves:', error);
        throw error;
      })
    );
  }

  uploadSaveFile(metadata: SaveFile, file: File): Observable<GameSaveExtended> {
    const formData = new FormData();
    formData.append('SaveFile', file);
    formData.append('Metadata', JSON.stringify(metadata));

    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`
        });

        return this.http.post<GameSaveExtended>(`${this.apiUrl}/saves`, formData, { headers });
      }),
      catchError(error => {
        console.error('Failed to retrieve saves:', error);
        throw error;
      })
    );
  }

  updateSaveFile(metadata: SaveFile | null, file: File | null, save: GameSaveExtended): Observable<GameSaveExtended> {
    const formData = new FormData();
    if (file) formData.append('SaveFile', file);
    if (metadata) formData.append('Metadata', JSON.stringify(metadata));

    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`
        });

        return this.http.put<GameSaveExtended>(`${this.apiUrl}/saves/${save.id}`, formData, { headers });
      }),
      catchError(error => {
        console.error('Failed to retrieve saves:', error);
        throw error;
      })
    );
  }

  deleteSaveFile(id: string): Observable<void> {
    return this.auth.getAccessTokenSilently().pipe(
      switchMap(token => {
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        });

        return this.http.delete<void>(
          `${this.apiUrl}/saves/${id}`,
          { headers }
        );
      }),
      catchError(error => {
        console.error('Failed to retrieve saves:', error);
        throw error;
      })
    );
  }
}
