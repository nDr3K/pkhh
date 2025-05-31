export interface GameSave {
  id: number;
  game: string;
  name: string;
  team: GamePokemon[];
  createdAt: string;
  updatedAt: string;
}

export interface GamePokemon {
  id: number;
  dexNumber: number;
  name: string;
}
