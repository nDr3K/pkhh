import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaveService } from "../../core/services/save.service";
import { GameSave } from "../../core/models/game-save";
import { GameSaveCardComponent } from "./components/game-save-card.component";
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { map, Observable } from "rxjs";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, GameSaveCardComponent, PageLayoutComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  private saveService = inject(SaveService);

  gameSaves$: Observable<GameSave[]> | undefined;

  ngOnInit(): void {
    this.gameSaves$ = this.saveService.getSaves().pipe(
      map(response => response.data)
    );
  }


  trackBySave(index: number, save: GameSave): number {
    return save.id;
  }
}
