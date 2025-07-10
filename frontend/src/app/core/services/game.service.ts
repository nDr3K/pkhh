import { Injectable } from "@angular/core";
import { BaseService } from "./base-service";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { GameRom } from "../models/game-rom";

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
}
