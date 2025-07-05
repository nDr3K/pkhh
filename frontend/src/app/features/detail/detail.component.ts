import { Component, inject } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { ActivatedRoute, Router } from "@angular/router";
import { SaveService } from "../../core/services/save.service";
import { Subject, takeUntil } from "rxjs";
import { GameSaveExtended } from "../../core/models/game-save";
import { PageLayoutComponent } from "../../shared/components/page-layout.component";

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent, NgOptimizedImage],
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent {
  gameId: string | null = null;
  gameSave: GameSaveExtended | null = null;
  selectedFile: File | null = null;
  isLoading = true;
  isDeleting = false;
  activeTab = 'summary';

  private route: ActivatedRoute = inject(ActivatedRoute);
  private router: Router = inject(Router);
  private saveService: SaveService = inject(SaveService);

  private destroy$ = new Subject<void>();

  ngOnInit(): void {
    this.gameId = this.route.snapshot.paramMap.get('id');
    if (this.gameId) {
      this.loadSaveFile(this.gameId).then();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  async loadSaveFile(id: string): Promise<void> {
    try {
      this.saveService.getSaveFileById(id)
        .pipe(takeUntil(this.destroy$))
        .subscribe(game => this.gameSave = game);
    } catch (error) {
      console.error('Failed to load save file:', error);
      await this.router.navigate(['/dashboard']);
    } finally {
      this.isLoading = false;
    }
  }

  async handleDelete(): Promise<void> {
    if (!this.gameSave || !this.gameId) return;
    if (!confirm('Are you sure you want to delete this save file?')) {
      return;
    }

    this.isDeleting = true;
    try {
      this.saveService.deleteSaveFile(this.gameId)
        .pipe(takeUntil(this.destroy$))
        .subscribe(() => this.router.navigate(['/dashboard']));
    } catch (error) {
      console.error('Failed to delete save file:', error);
      this.isDeleting = false;
    }
  }

  async handleUpdate(): Promise<void> {
    if (!this.gameSave) return;

    try {
      this.saveService.updateSaveFile(null, this.selectedFile, this.gameSave)
        .pipe(takeUntil(this.destroy$))
        .subscribe(game => this.gameSave = game);
    } catch (error) {
      console.error('Failed to update save file:', error);
    }
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }

  getBadgeVariant(game: string): string {
    return game.includes('Scarlet') || game.includes('Red') ? 'destructive' : 'default';
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }

  getPokemonImageUrl(dexNumber: number): string {
    // Using PokeAPI sprites
    return `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-i/red-blue/${dexNumber}.png`
  }

  // handleToggleFavorite(): void {
  //   if (!this.gameSave) return;
  //   this.gameSave.isFavorite = !this.gameSave.isFavorite;
  //   this.saveService.updateSaveFile({isFavorite: true}, null, this.gameSave)
  //     .pipe(takeUntil(this.destroy$))
  //     .subscribe(game => this.gameSave = game);
  // }
}
