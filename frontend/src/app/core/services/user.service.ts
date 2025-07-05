import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { UserProfile } from "../models/user-profile";
import { BaseService } from "./base-service";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  private apiUrl = environment.api.serverUrl;

  authenticateWithBackend(): Observable<UserProfile> {
    return this.makeAuthenticatedRequest<UserProfile>(
      'GET',
      `${this.apiUrl}/auth/login`
    );
  }
}
