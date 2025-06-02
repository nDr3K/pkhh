export interface SaveFile {
  gameId: number;
  name?: string;
  description?: string;
  tags?: string[];
}

export interface SaveFileCreateRequest {
  metadata: SaveFile;
  file: File;
}
