import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { GameRomMetadata, RomOffsetsMap } from "../../../../core/models/game-rom";
import { finalize } from "rxjs";
import { GameService } from "../../../../core/services/game.service";

@Component({
  selector: 'app-rom-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './rom-modal.component.html',
  styleUrls: ['./rom-modal.component.scss']
})
export class RomModalComponent {
  @Input() isVisible = false;
  @Output() closeEvent = new EventEmitter<void>();
  @Output() uploadSuccess = new EventEmitter<GameRomMetadata>();

  private gameService = inject(GameService);

  generations = Array.from({length: 9}, (_, i) => i + 1); // [1, 2, 3, 4, 5, 6, 7, 8, 9]

  uploadForm: FormGroup;
  selectedFile: File | null = null;
  isUploading = false;
  errorMessage = '';
  formSubmitted = false;

  constructor(private fb: FormBuilder) {
    this.uploadForm = this.fb.group({
      name: [''],
      generation: [''],
      official: [false],
      region: [''],
      // Moves offsets
      movesOffset: [''],
      movesEntryLength: ['', [Validators.min(1)]],
      movesCount: ['', [Validators.min(1)]],
      // Move names offsets
      moveNamesOffset: [''],
      moveNamesEntryLength: ['', [Validators.min(1)]],
      moveNamesCount: ['', [Validators.min(1)]],
      // Pokemon stats offsets
      pokemonStatsOffset: [''],
      pokemonStatsEntryLength: ['', [Validators.min(1)]],
      pokemonStatsCount: ['', [Validators.min(1)]],
      // Pokemon names offsets
      pokemonNamesOffset: [''],
      pokemonNamesEntryLength: ['', [Validators.min(1)]],
      pokemonNamesCount: ['', [Validators.min(1)]],
      // Pokedex offsets
      pokedexOffset: [''],
      pokedexEntryLength: ['', [Validators.min(1)]],
      pokedexCount: ['', [Validators.min(1)]],
      // Types offsets
      typesOffset: [''],
      typesEntryLength: ['', [Validators.min(1)]],
      typesCount: ['', [Validators.min(1)]]
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.errorMessage = '';

      // Autofill name from filename if not already filled
      if (!this.uploadForm.get('name')?.value) {
        const nameWithoutExtension = this.selectedFile.name.replace(/\.[^/.]+$/, '');
        this.uploadForm.patchValue({ name: nameWithoutExtension });
      }
    }
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    const dropZone = event.currentTarget as HTMLElement;
    dropZone.classList.add('drag-over');
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    const dropZone = event.currentTarget as HTMLElement;
    dropZone.classList.remove('drag-over');
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    const dropZone = event.currentTarget as HTMLElement;
    dropZone.classList.remove('drag-over');

    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      const file = files[0];
      // Check the file type
      const allowedTypes = ['.gba', '.nds', '.gb', '.gbc'];
      const fileExtension = '.' + file.name.split('.').pop()?.toLowerCase();

      if (allowedTypes.includes(fileExtension)) {
        this.selectedFile = file;
        this.errorMessage = '';

        // Autofill name from filename if not already filled
        if (!this.uploadForm.get('name')?.value) {
          const nameWithoutExtension = file.name.replace(/\.[^/.]+$/, '');
          this.uploadForm.patchValue({ name: nameWithoutExtension });
        }
      } else {
        this.errorMessage = 'Please select a valid ROM file (.gba, .nds, .gb, .gbc)';
      }
    }
  }

  removeFile(event: Event): void {
    event.stopPropagation();
    this.selectedFile = null;
    this.errorMessage = '';

    const input = document.getElementById('fileInput') as HTMLInputElement;
    if (input) {
      input.value = '';
    }
  }

  onSubmit(): void {
    this.formSubmitted = true;

    if (!this.selectedFile) {
      this.errorMessage = 'Please select a ROM file to upload';
      return;
    }

    if (this.isUploading) {
      return;
    }

    this.isUploading = true;
    this.errorMessage = '';

    const formValue = this.uploadForm.value;

    // Build the offsets map
    const offsets: RomOffsetsMap = {
      moves: {
        offset: this.parseOffset(formValue.movesOffset),
        entryLength: Number(formValue.movesEntryLength) || 0,
        count: Number(formValue.movesCount) || 0
      },
      moveNames: {
        offset: this.parseOffset(formValue.moveNamesOffset),
        entryLength: Number(formValue.moveNamesEntryLength) || 0,
        count: Number(formValue.moveNamesCount) || 0
      },
      pokemonStats: {
        offset: this.parseOffset(formValue.pokemonStatsOffset),
        entryLength: Number(formValue.pokemonStatsEntryLength) || 0,
        count: Number(formValue.pokemonStatsCount) || 0
      },
      pokemonNames: {
        offset: this.parseOffset(formValue.pokemonNamesOffset),
        entryLength: Number(formValue.pokemonNamesEntryLength) || 0,
        count: Number(formValue.pokemonNamesCount) || 0
      },
      pokedex: {
        offset: this.parseOffset(formValue.pokedexOffset),
        entryLength: Number(formValue.pokedexEntryLength) || 0,
        count: Number(formValue.pokedexCount) || 0
      },
      types: {
        offset: this.parseOffset(formValue.typesOffset),
        entryLength: Number(formValue.typesEntryLength) || 0,
        count: Number(formValue.typesCount) || 0
      }
    };

    const metadata: GameRomMetadata = {
      offsets,
      name: formValue.name || this.selectedFile.name,
      generation: formValue.generation ? Number(formValue.generation) : undefined,
      official: formValue.official || false,
      region: formValue.region || undefined
    };

    this.gameService.uploadRomFile(metadata, this.selectedFile)
      .pipe(finalize(() => this.isUploading = false))
      .subscribe({
        next: (_) => {
          this.uploadSuccess.emit();
          this.resetForm();
          this.closeModal();
        },
        error: (error) => {
          console.error('ROM upload failed:', error);
          this.errorMessage = error.error?.detail || error.message || 'ROM upload failed. Please try again.';
        }
      });
  }

  formatFileSize(bytes: number): string {
    if (bytes === 0) return '0 Bytes';
    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
  }

  private parseOffset(offsetString: string): number {
    if (!offsetString) return 0;

    const cleanString = offsetString.trim();

    // Handle hex values (0x prefix or just hex digits)
    if (cleanString.toLowerCase().startsWith('0x')) {
      return parseInt(cleanString, 16);
    }

    // Check if it looks like hex (only contains 0-9, a-f, A-F)
    const hexPattern = /^[0-9a-fA-F]+$/;
    if (hexPattern.test(cleanString)) {
      return parseInt(cleanString, 16);
    }

    // Otherwise treat as decimal
    return parseInt(cleanString, 10) || 0;
  }

  closeModal(): void {
    if (!this.isUploading) {
      this.resetForm();
      this.closeEvent.emit();
    }
  }

  private resetForm(): void {
    this.uploadForm.reset();
    this.uploadForm.patchValue({ official: false }); // Reset checkbox to false
    this.selectedFile = null;
    this.errorMessage = '';
    this.formSubmitted = false;
  }
}
