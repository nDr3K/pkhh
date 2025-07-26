import { Injectable } from "@angular/core";
import { canManageRoms, UserProfile } from "../models/user-profile";

@Injectable({
  providedIn: 'root'
})
export class UserStateService {
  user: UserProfile | null = null;

  setUser(user: UserProfile) {
    this.user = user;
  }

  canManageRoms(): boolean {
    if (!this.user) return false;
    return canManageRoms(this.user);
  }
}
