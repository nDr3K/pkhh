import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { BehaviorSubject } from "rxjs";
import { GameRom } from "../../core/models/game-rom";
import { GameService } from "../../core/services/game.service";
import { Router } from "@angular/router";
import { UserStateService } from "../../core/services/user-state.service";

@Component({
  selector: 'app-roms',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent, NgOptimizedImage],
  templateUrl: './roms.component.html',
  styleUrls: ['./roms.component.scss']
})
export class RomsComponent implements OnInit {
  romGames$ = new BehaviorSubject<GameRom[]>([]);
  hasPermission: boolean = false;

  private readonly userStateService: UserStateService = inject(UserStateService);
  private readonly gameService: GameService = inject(GameService);
  private readonly router = inject(Router);

  public ngOnInit(): void {
    this.gameService.getRoms().subscribe(response => {
      this.romGames$.next(response);
    });
    this.hasPermission = this.userStateService.canManageRoms();
  }

  trackByGame(index: number, rom: GameRom): number {
    return rom.id;
  }

  onGameClick(game: GameRom): void {
    this.router.navigate([`/emulator`], {
      state: { game: game }
    }).then();
  }

  openUploadModal(): void {

  }
}
