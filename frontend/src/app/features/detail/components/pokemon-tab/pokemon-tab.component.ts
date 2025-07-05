
import { Component, Input } from '@angular/core';
import { GameSaveExtended } from "../../../../core/models/game-save";
import { CommonModule, NgOptimizedImage } from "@angular/common";

@Component({
  selector: 'app-pokemon-tab',
  templateUrl: './pokemon-tab.component.html',
  styleUrls: ['./pokemon-tab.component.scss'],
  standalone: true,
  imports: [CommonModule, NgOptimizedImage]
})
export class PokemonTabComponent {
  @Input() gameSave: GameSaveExtended | null = null;

  getPokemonImageUrl(dexNumber: number): string {
    return `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/${dexNumber}.png`;
  }
}
