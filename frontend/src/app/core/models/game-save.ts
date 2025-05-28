export interface GameSave {
  id: number;
  game: string;
  name: string;
  team: GamePokemon[];
  createdAt: Date;
  updatedAt: Date;
}

export interface GamePokemon {
  id: number;
  dexNumber: number;
  name: string;
}
