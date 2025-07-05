
import { Component, Input } from '@angular/core';
import { GameSaveExtended } from "../../../../core/models/game-save";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-detail-header',
  templateUrl: './detail-header.component.html',
  styleUrls: ['./detail-header.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class DetailHeaderComponent {
  @Input() gameSave: GameSaveExtended | null = null;

  getBadgeVariant(game: string): string {
    return game.includes('Scarlet') || game.includes('Red') ? 'destructive' : 'default';
  }
}
