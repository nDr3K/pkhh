import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { GameSave, GameSaveExtended } from "../models/game-save";
import { PagedResponse } from "../models/paged-response";
import { SaveFile } from "../models/save-file";
import { BaseService } from "./base-service";

@Injectable({
  providedIn: 'root'
})
export class SaveService extends BaseService {
  private apiUrl = environment.api.serverUrl;

  getSaves(pageNumber: number = 1, pageSize: number = 10): Observable<PagedResponse<GameSave>> {
    return this.makeAuthenticatedRequest<PagedResponse<GameSave>>(
      'GET',
      `${this.apiUrl}/saves?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  getSaveFileById(id: string): Observable<GameSaveExtended> {
    return this.makeAuthenticatedRequest<GameSaveExtended>(
      'GET',
      `${this.apiUrl}/saves/${id}`
    );
  }

  uploadSaveFile(metadata: SaveFile, file: File): Observable<GameSaveExtended> {
    const formData = new FormData();
    formData.append('SaveFile', file);
    formData.append('Metadata', JSON.stringify(metadata));

    return this.makeAuthenticatedRequest<GameSaveExtended>(
      'POST',
      `${this.apiUrl}/saves`,
      { body: formData }
    );
  }

  updateSaveFile(metadata: SaveFile | null, file: File | null, save: GameSaveExtended): Observable<GameSaveExtended> {
    const formData = new FormData();
    if (file) formData.append('SaveFile', file);
    if (metadata) formData.append('Metadata', JSON.stringify(metadata));

    return this.makeAuthenticatedRequest<GameSaveExtended>(
      'PUT',
      `${this.apiUrl}/saves/${save.id}`,
      { body: formData }
    );
  }

  deleteSaveFile(id: string): Observable<void> {
    return this.makeAuthenticatedRequest<void>(
      'DELETE',
      `${this.apiUrl}/saves/${id}`,
    );
  }
}
