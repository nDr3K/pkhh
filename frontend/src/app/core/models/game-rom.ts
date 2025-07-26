export interface GameRom {
  id: number;
  name: string;
  path: string;
  generation: number;
  official: boolean;
  region: string;
}

export interface GameRomMetadata {
  offsets: RomOffsetsMap;
  name?: string;
  generation?: number;
  official?: boolean;
  region?: string;
}

export interface DataSection {
  offset: number;
  entryLength: number;
  count: number;
}

export interface RomOffsetsMap {
  moves: DataSection;
  moveNames: DataSection;
  pokemonStats: DataSection;
  pokemonNames: DataSection;
  pokedex: DataSection;
  types: DataSection;
}
