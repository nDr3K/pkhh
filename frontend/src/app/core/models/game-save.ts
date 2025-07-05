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

export interface GameSaveExtended extends GameSave {
  description: string;
  tags: string[];
  playTime: string;
  badges: number[];
  isFavorite: boolean;
  playerName: string;
  pokemonSeen: number;
  pokemonCaught: number;
  pokemonTotal: number;
  team: GamePokemonExtended[];
  boxes: GameBox[];
}

export interface GamePokemonExtended extends GamePokemon {
  type1: string;
  type2: string;
  ability: string;
  level: number;
  nature: string;
  move1: string;
  move2: string;
  move3: string;
  move4: string;
}

export interface GameBox {
  id: number;
  name: string;
  capacity: number;
  count: number;
  pokemons: GamePokemonExtended[];
}
