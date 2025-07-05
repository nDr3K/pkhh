export interface SaveFile {
  gameId?: number;
  name?: string;
  description?: string;
  tags?: string[];
  isFavorite?: boolean;
}

export interface SaveFileCreateRequest {
  metadata: SaveFile;
  file: File;
}
