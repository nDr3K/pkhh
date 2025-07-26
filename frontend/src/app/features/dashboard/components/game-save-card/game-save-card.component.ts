import { Component, inject, Input } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { GamePokemon, GameSave } from "../../../../core/models/game-save";
import { Router } from "@angular/router";

@Component({
  selector: 'app-game-save-card',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  templateUrl: './game-save-card.component.html',
  styleUrls: ['./game-save-card.component.scss']
})
export class GameSaveCardComponent {
  @Input() gameSave!: GameSave;
  private router = inject(Router);

  private gameColors: { [key: string]: string } = {
    'red': '#dc3545',
    'blue': '#007bff',
    'yellow': '#ffc107',
    'green': '#32cd32',
  };

  getGameColor(game: string): string {
    const gameKey = game.toLowerCase().replace(/\s+/g, '');
    return this.gameColors[gameKey] || '#6c757d';
  }

  getGameClass(game: string): string {
    return game.toLowerCase().replace(/\s+/g, '');
  }

  getPokemonImageUrl(dexNumber: number): string {
    // Using PokeAPI sprites
    return `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/${dexNumber}.png`
  }

  onImageError(event: any): void {
    // Fallback to a placeholder if the Pokémon image fails to load
    event.target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDgiIGhlaWdodD0iNDgiIHZpZXdCb3g9IjAgMCA0OCA0OCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPGNpcmNsZSBjeD0iMjQiIGN5PSIyNCIgcj0iMjQiIGZpbGw9IiNmOGY5ZmEiLz4KPHN2ZyB4PSIxMiIgeT0iMTIiIHdpZHRoPSIyNCIgaGVpZ2h0PSIyNCIgdmlld0JveD0iMCAwIDI0IDI0IiBmaWxsPSJub25lIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPgo8cGF0aCBkPSJNMTIgMkM2LjQ4IDIgMiA2LjQ4IDIgMTJzNC40OCAxMCAxMCAxMCAxMC00LjQ4IDEwLTEwUzE3LjUyIDIgMTIgMnptLTIgMTVsLTUtNSAxLjQxLTEuNDFMMTAgMTQuMTdsNy41OS03LjU5TDE5IDhsLTkgOXoiIGZpbGw9IiM2Yzc1N2QiLz4KPC9zdmc+Cjwvc3ZnPgo=';
  }

  getEmptySlots(): number[] {
    const emptyCount = Math.max(0, 6 - this.gameSave.team.length);
    return Array(emptyCount).fill(0).map((_, i) => i);
  }

  trackByPokemon(index: number, pokemon: GamePokemon): number {
    return pokemon.id;
  }

  trackByIndex(index: number): number {
    return index;
  }

  formatDate(dateStr: string): string {
    const date = new Date(dateStr);
    const now = new Date();
    const diffTime = Math.abs(now.getTime() - date.getTime());
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

    if (diffDays === 1) {
      return 'Yesterday';
    } else if (diffDays < 7) {
      return `${diffDays} days ago`;
    } else if (diffDays < 30) {
      const weeks = Math.floor(diffDays / 7);
      return `${weeks} week${weeks > 1 ? 's' : ''} ago`;
    } else {
      return date.toLocaleDateString('en-US', {
        month: 'short',
        day: 'numeric',
        year: date.getFullYear() !== now.getFullYear() ? 'numeric' : undefined
      });
    }
  }

  onDetailsClick(): void {
    this.router.navigate(['/detail', this.gameSave.id]).then();
  }
}
