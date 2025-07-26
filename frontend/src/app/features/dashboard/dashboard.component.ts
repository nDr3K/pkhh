import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaveService } from "../../core/services/save.service";
import { GameSave } from "../../core/models/game-save";
import { GameSaveCardComponent } from "./components/game-save-card/game-save-card.component";
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { BehaviorSubject, map, Observable } from "rxjs";
import { SaveFileModalComponent } from "./components/save-file-modal/save-file-modal.component";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, GameSaveCardComponent, PageLayoutComponent, SaveFileModalComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  private saveService = inject(SaveService);

  gameSaves$ = new BehaviorSubject<GameSave[]>([]);
  isModalVisible: boolean = false;

  ngOnInit(): void {
    this.saveService.getSaves().subscribe(response => {
      this.gameSaves$.next(response.data);
    });
  }

  trackBySave(index: number, save: GameSave): number {
    return save.id;
  }

  // Modal
  openUploadModal(): void {
    this.isModalVisible = true;
  }

  onModalClose(): void {
    this.isModalVisible = false;
  }

  onUploadSuccess(response: GameSave): void {
    this.onModalClose();
    this.gameSaves$.next([...this.gameSaves$.value, response]);
  }
}
