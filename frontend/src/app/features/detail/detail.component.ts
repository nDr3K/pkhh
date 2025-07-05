import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { SaveService } from '../../core/services/save.service';
import { Subject, takeUntil } from 'rxjs';
import { GameSaveExtended } from '../../core/models/game-save';
import { PageLayoutComponent } from '../../shared/components/page-layout.component';
import { DetailHeaderComponent } from './components/detail-header/detail-header.component';
import { DetailTabsComponent } from './components/detail-tabs/detail-tabs.component';
import { SummaryTabComponent } from './components/summary-tab/summary-tab.component';
import { PokemonTabComponent } from './components/pokemon-tab/pokemon-tab.component';
import { BoxesTabComponent } from './components/boxes-tab/boxes-tab.component';
import { ProgressTabComponent } from './components/progress-tab/progress-tab.component';
import { ActionsSidebarComponent } from './components/actions-sidebar/actions-sidebar.component';
import { FileInfoSidebarComponent } from './components/file-info-sidebar/file-info-sidebar.component';

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [
    CommonModule,
    PageLayoutComponent,
    DetailHeaderComponent,
    DetailTabsComponent,
    SummaryTabComponent,
    PokemonTabComponent,
    BoxesTabComponent,
    ProgressTabComponent,
    ActionsSidebarComponent,
    FileInfoSidebarComponent,
  ],
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
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
      this.saveService
        .getSaveFileById(id)
        .pipe(takeUntil(this.destroy$))
        .subscribe(game => (this.gameSave = game));
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
      this.saveService
        .deleteSaveFile(this.gameId)
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
      this.saveService
        .updateSaveFile(null, this.selectedFile, this.gameSave)
        .pipe(takeUntil(this.destroy$))
        .subscribe(game => (this.gameSave = game));
    } catch (error) {
      console.error('Failed to update save file:', error);
    }
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }
}
