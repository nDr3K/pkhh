
import { Component, Input } from '@angular/core';
import { GameSaveExtended } from "../../../../core/models/game-save";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-boxes-tab',
  templateUrl: './boxes-tab.component.html',
  styleUrls: ['./boxes-tab.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class BoxesTabComponent {
  @Input() gameSave: GameSaveExtended | null = null;
}
