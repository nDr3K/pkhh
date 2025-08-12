import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { SaveService } from "../../../../core/services/save.service";
import { SaveFile } from "../../../../core/models/save-file";
import { finalize } from "rxjs";
import { GameSave } from "../../../../core/models/game-save";
import { Game } from "../../../../core/models/game-rom";

@Component({
  selector: 'app-save-file-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './save-file-modal.component.html',
  styleUrls: ['./save-file-modal.component.scss']
})
export class SaveFileModalComponent {
  @Input() isVisible = false;
  @Input() games: Game[] | null = [];
  @Output() closeEvent = new EventEmitter<void>();
  @Output() uploadSuccess = new EventEmitter<GameSave>();

  private saveService = inject(SaveService);

  uploadForm: FormGroup;
  selectedFile: File | null = null;
  isUploading = false;
  errorMessage = '';
  formSubmitted = false;

  constructor(private fb: FormBuilder) {
    this.uploadForm = this.fb.group({
      name: [''],
      game: [''],
      description: [''],
      tagsInput: ['']
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
      const allowedTypes = ['.sav'];
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
        this.errorMessage = 'Please select a valid save file (.sav)';
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
      this.errorMessage = 'Please select a file to upload';
      return;
    }

    if (this.isUploading) {
      return;
    }

    this.isUploading = true;
    this.errorMessage = '';

    const metadata: SaveFile = {
      gameId: +this.uploadForm.get('game')?.value, //
      name: this.uploadForm.get('name')?.value || this.selectedFile.name,
      description: this.uploadForm.get('description')?.value || undefined,
      tags: [] //this.parseTags(this.uploadForm.get('tagsInput')?.value)
    };

    this.saveService.uploadSaveFile(metadata, this.selectedFile)
      .pipe(finalize(() => this.isUploading = false))
      .subscribe({
        next: (response) => {
          this.uploadSuccess.emit(response);
          this.resetForm();
          this.closeModal();
        },
        error: (error) => {
          console.error('Upload failed:', error);
          this.errorMessage = error.error?.detail || error.message || 'Upload failed. Please try again.';
        }
      });
  }

  // private parseTags(tagsInput: string): string[] {
  //   if (!tagsInput) return [];
  //   return tagsInput.split(',').map(tag => tag.trim()).filter(tag => tag.length > 0);
  // }

  formatFileSize(bytes: number): string {
    if (bytes === 0) return '0 Bytes';
    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
  }

  closeModal(): void {
    if (!this.isUploading) {
      this.resetForm();
      this.closeEvent.emit();
    }
  }

  private resetForm(): void {
    this.uploadForm.reset();
    this.selectedFile = null;
    this.errorMessage = '';
    this.formSubmitted = false;
  }
}
