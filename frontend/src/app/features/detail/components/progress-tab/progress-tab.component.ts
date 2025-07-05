
import { Component, Input } from '@angular/core';
import { GameSaveExtended } from "../../../../core/models/game-save";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-progress-tab',
  templateUrl: './progress-tab.component.html',
  styleUrls: ['./progress-tab.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class ProgressTabComponent {
  @Input() gameSave: GameSaveExtended | null = null;
}
