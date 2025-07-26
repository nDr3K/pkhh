import { GameSave } from "./game-save";

export interface UserProfile {
  email: string;
  name: string;
  permissions: string[];
}

export function canManageRoms(user: UserProfile): boolean {
  return user.permissions.includes("manage:roms");
}
