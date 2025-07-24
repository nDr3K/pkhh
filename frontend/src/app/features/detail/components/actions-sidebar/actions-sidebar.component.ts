
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GameSaveExtended } from "../../../../core/models/game-save";
import { NgIf } from "@angular/common";

@Component({
  selector: 'app-actions-sidebar',
  templateUrl: './actions-sidebar.component.html',
  styleUrls: ['./actions-sidebar.component.scss'],
  imports: [
    NgIf
  ],
  standalone: true
})
export class ActionsSidebarComponent {
  @Input() gameSave: GameSaveExtended | null = null;
  @Input() isDeleting = false;
  @Output() continue = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();
  @Output() update = new EventEmitter<void>();

  handleContinue(): void {
    this.continue.emit();
  }

  handleUpdate(): void {
    this.update.emit();
  }

  handleDelete(): void {
    this.delete.emit();
  }
}
