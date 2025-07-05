
import { Component, Input } from '@angular/core';
import { GameSaveExtended } from "../../../../core/models/game-save";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-summary-tab',
  templateUrl: './summary-tab.component.html',
  styleUrls: ['./summary-tab.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class SummaryTabComponent {
  @Input() gameSave: GameSaveExtended | null = null;

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }
}
