import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { BehaviorSubject } from "rxjs";
import { GameRom } from "../../core/models/game-rom";
import { GameService } from "../../core/services/game.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-roms',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent, NgOptimizedImage],
  templateUrl: './roms.component.html',
  styleUrls: ['./roms.component.scss']
})
export class RomsComponent implements OnInit {
  romGames$ = new BehaviorSubject<GameRom[]>([]);

  private readonly gameService: GameService = inject(GameService);
  private readonly router = inject(Router);

  public ngOnInit(): void {
    this.gameService.getRoms().subscribe(response => {
      this.romGames$.next(response);
    });
  }

  trackByGame(index: number, rom: GameRom): number {
    return rom.id;
  }

  onGameClick(game: GameRom): void {
    this.router.navigate([`/emulator`], {
      state: { game: game }
    }).then();
  }
}
