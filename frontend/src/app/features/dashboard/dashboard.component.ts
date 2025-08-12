import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaveService } from "../../core/services/save.service";
import { GameSave } from "../../core/models/game-save";
import { GameSaveCardComponent } from "./components/game-save-card/game-save-card.component";
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { BehaviorSubject, Observable } from "rxjs";
import { SaveFileModalComponent } from "./components/save-file-modal/save-file-modal.component";
import { GameService } from "../../core/services/game.service";
import { Game } from "../../core/models/game-rom";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, GameSaveCardComponent, PageLayoutComponent, SaveFileModalComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  private saveService = inject(SaveService);
  private gameService = inject(GameService);

  gameSaves$ = new BehaviorSubject<GameSave[]>([]);
  games$: Observable<Game[]> | undefined;
  isModalVisible: boolean = false;

  ngOnInit(): void {
    this.saveService.getSaves().subscribe(response => {
      this.gameSaves$.next(response.data);
    });
    this.games$ = this.gameService.getRoms();
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
