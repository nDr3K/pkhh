import { Injectable } from "@angular/core";
import { BaseService } from "./base-service";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { GameRom, GameRomMetadata } from "../models/game-rom";
import { GameSaveExtended } from "../models/game-save";

@Injectable({
  providedIn: 'root'
})
export class GameService extends BaseService {
  private apiUrl = environment.api.serverUrl;

  getRoms(): Observable<GameRom[]> {
    return this.makeAuthenticatedRequest<GameRom[]>(
      'GET',
      `${this.apiUrl}/games`
    );
  }

  uploadRomFile(metadata: GameRomMetadata, file: File): Observable<GameSaveExtended> {
    const formData = new FormData();
    formData.append('RomFile', file);
    formData.append('Metadata', JSON.stringify(metadata));

    return this.makeAuthenticatedRequest<GameSaveExtended>(
      'POST',
      `${this.apiUrl}/roms`,
      { body: formData }
    );
  }
}
