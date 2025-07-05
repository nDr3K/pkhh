
import { Component, Input } from '@angular/core';
import { GameSaveExtended } from "../../../../core/models/game-save";
import { NgIf } from "@angular/common";

@Component({
  selector: 'app-file-info-sidebar',
  templateUrl: './file-info-sidebar.component.html',
  styleUrls: ['./file-info-sidebar.component.scss'],
  imports: [
    NgIf
  ],
  standalone: true
})
export class FileInfoSidebarComponent {
  @Input() gameSave: GameSaveExtended | null = null;

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }
}
