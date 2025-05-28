import { Injectable } from "@angular/core";
import { UserProfile } from "../models/user-profile";

@Injectable({
  providedIn: 'root'
})
export class UserStateService {
  user: UserProfile | null = null;

  setUser(user: UserProfile) {
    this.user = user;
  }
}
