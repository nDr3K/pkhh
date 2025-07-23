import { Component, ElementRef, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageLayoutComponent } from "../../shared/components/page-layout.component";
import { EmulatorConfig, EmulatorLoaderService } from './service/emulator-loader.service';
import { Router } from "@angular/router";
import { GameRom } from "../../core/models/game-rom";
import { environment } from "../../../environments/environment";
import { SaveService } from "../../core/services/save.service";
import { GameSaveExtended } from "../../core/models/game-save";
import { SaveFile } from "../../core/models/save-file";

@Component({
  selector: 'app-emulator',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent],
  templateUrl: './emulator.component.html',
  styleUrls: ['./emulator.component.scss']
})
export class EmulatorComponent implements OnInit, OnDestroy {
  @ViewChild('gameContainer', { static: true }) gameContainer!: ElementRef<HTMLDivElement>;

  private emulatorColor = '#74b9ff';
  private debug = true;//false; TODO()
  romUrl: string | undefined;
  gameRom: GameRom | undefined;
  gameSave: GameSaveExtended | undefined;

  gameRunning = false;
  status = 'Ready';
  error: string | null = null;

  private emulatorInstance: any = null;
  private emulatorService = inject(EmulatorLoaderService);
  private saveService = inject(SaveService);
  private router = inject(Router);

  constructor() {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as { game?: GameRom };
    if (state?.game) {
      this.gameRom = state.game;
      this.romUrl = `${environment.rom.url}${state.game.path}`;
    }
    console.log('EmulatorComponent created', state);
  }

  async ngOnInit() {
    console.log('EmulatorComponent mounted');

    // Small delay to ensure DOM is ready and cleanup is complete
    setTimeout(() => {
      this.initializeEmulator();
    }, 0);
  }

  ngOnDestroy() {
    console.log('EmulatorComponent destroyed');
    this.cleanup();

    // Additional cleanup after a delay to ensure everything is removed
    setTimeout(() => {
      this.forceCleanupRemainingElements();
    }, 100);
  }

  protected async initializeEmulator(): Promise<void> {
    try {
      this.status = 'Initializing...';
      this.error = null;

      // Clear container
      const container = this.gameContainer.nativeElement;
      container.innerHTML = '';

      // Generate unique container ID
      if (!container.id) {
        container.id = `emulator-${Date.now()}`;
      }

      if(!this.romUrl) {
        return;
      }

      // Configure emulator
      const config: EmulatorConfig = {
        player: `#${container.id}`,
        gameUrl: this.romUrl,
        core: this.detectCore(this.romUrl),
        pathtodata: 'https://cdn.emulatorjs.org/stable/data/',
        gameID: this.generateGameID(),
        gameName: this.extractRomName(this.romUrl),
        color: this.emulatorColor,
        debug: this.debug,
        VirtualGamepadSettings: {},
        onGameStart: () => {
          this.gameRunning = true;
          this.status = 'Running';
          console.log('Game started');
        },
        onReady: () => {
          this.status = 'Ready';
          console.log('Emulator ready');
        },
        onSaveSave: (save: any) => {
          console.log('Save save', save.save);
          this.saveGame(save.save);
        },
        onLoadSave: () => {
          console.log('Load save');
        },
        buttonOpts: {
          saveState: false,
          loadState: false,
          cacheManager: false,
          exitEmulation: false,
          cheat: false,
        }
      };

      this.status = 'Loading EmulatorJS...';

      // Initialize emulator using service
      this.emulatorInstance = await this.emulatorService.initializeEmulator(config);

      this.emulatorInstance.on('saveSaveFiles', (save: any) => {
        console.log('Save saveFiles', save);
      });

      this.status = 'Emulator loaded successfully';
      console.log('Emulator initialized successfully');

    } catch (error) {
      console.error('Failed to initialize emulator:', error);
      this.error = error instanceof Error ? error.message : 'Unknown error occurred';
      this.status = 'Error';
    }
  }

  private detectCore(romUrl: string): string {
    const extension = romUrl.split('.').pop()?.toLowerCase();

    // Core detection based on file extension
    const coreMap: { [key: string]: string } = {
      'gb': 'gb',
      'gbc': 'gb',
      'gba': 'gba',
      'nes': 'nes',
      'snes': 'snes',
      'smc': 'snes',
      'sfc': 'snes',
      'n64': 'n64',
      'z64': 'n64',
      'nds': 'nds',
      'gen': 'segaMD',
      'md': 'segaMD',
      'smd': 'segaMD',
      'psx': 'psx',
      'cue': 'psx',
      'bin': 'psx',
      'iso': 'psx'
    };

    return coreMap[extension || ''] || 'gb'; // Default to Game Boy
  }

  protected extractRomName(url: string): string {
    try {
      const filename = url.split('/').pop() || 'Unknown ROM';
      // Remove the file extension and clean up the name
      return filename.replace(/\.[^/.]+$/, '').replace(/[_-]/g, ' ');
    } catch {
      return 'Unknown ROM';
    }
  }

  private generateGameID(): string {
    return `game_${Date.now()}_${Math.random().toString(36).substring(2, 11)}`;
  }

  private cleanup(): void {
    console.log('Cleaning up emulator component...');

    // First, cleanup through the service
    if (this.emulatorInstance) {
      this.emulatorService.cleanup();
      this.emulatorInstance = null;
    }

    // Then, aggressively clean the container
    const container = this.gameContainer?.nativeElement;
    if (container) {
      // Find and remove all child elements
      const iframes = container.querySelectorAll('iframe');
      const canvases = container.querySelectorAll('canvas');
      const audioElements = container.querySelectorAll('audio, video');

      // Stop and remove iframes
      iframes.forEach(iframe => {
        console.log('Removing iframe:', iframe);
        try {
          if (iframe.contentWindow) {
            iframe.contentWindow.postMessage({ action: 'pause' }, '*');
          }
        } catch (error) {
          console.warn('Could not pause iframe:', error);
        }
        iframe.remove();
      });

      // Remove canvases
      canvases.forEach(canvas => {
        console.log('Removing canvas:', canvas);
        canvas.remove();
      });

      // Pause and remove audio/video elements
      audioElements.forEach(media => {
        const mediaElement = media as HTMLMediaElement;
        if (!mediaElement.paused) {
          mediaElement.pause();
        }
        mediaElement.src = '';
        mediaElement.load();
        mediaElement.remove();
      });

      // Clear the entire container
      container.innerHTML = '';

      // Remove any event listeners that might be attached
      const newContainer = container.cloneNode(false);
      container.parentNode?.replaceChild(newContainer, container);
    }

    // Reset component state
    this.gameRunning = false;
    this.status = 'Ready';
    this.error = null;
  }

  /**
   * Force cleanup of any remaining emulator elements in the DOM
   */
  private forceCleanupRemainingElements(): void {
    // Look for any remaining emulator elements across the entire document
    const selectors = [
      'iframe[src*="emulator"]',
      'iframe[src*="retroarch"]',
      'iframe[src*="wasm"]',
      'canvas[class*="emulator"]',
      'canvas[id*="emulator"]',
      'div[class*="emulator-container"]',
      'audio[src*="emulator"]',
      'video[src*="emulator"]'
    ];

    selectors.forEach(selector => {
      const elements = document.querySelectorAll(selector);
      elements.forEach(element => {
        console.log('Force removing remaining emulator element:', element);

        if (element.tagName === 'IFRAME') {
          const iframe = element as HTMLIFrameElement;
          try {
            if (iframe.contentWindow) {
              iframe.contentWindow.postMessage({ action: 'stop' }, '*');
            }
          } catch (error) {
            console.warn('Could not stop iframe:', error);
          }
        }

        if (element.tagName === 'AUDIO' || element.tagName === 'VIDEO') {
          const media = element as HTMLMediaElement;
          if (!media.paused) {
            media.pause();
          }
          media.src = '';
          media.load();
        }

        element.remove();
      });
    });
  }

  private saveGame(save: any) {
    console.log(save);
    if (!this.gameRom) return;
    let metadata: SaveFile = {
      gameId: this.gameRom.id,
      name: this.gameRom.name
    };
    let file = new File([save], 'save.sav', {
      type: 'application/octet-stream'
    });
    console.log(file);
    if (this.gameSave) {
      this.saveService.updateSaveFile(null,file,this.gameSave)
        .subscribe(game => (this.gameSave = game));
    } else {
      this.saveService.uploadSaveFile(metadata,file)
        .subscribe(game => (this.gameSave = game));
    }
  }
}
