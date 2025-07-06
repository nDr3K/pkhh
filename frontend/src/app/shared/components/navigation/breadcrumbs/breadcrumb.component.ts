import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface BreadcrumbItem {
  label: string;
  url?: string;
  icon?: string;
  disabled?: boolean;
  data?: any;
}

@Component({
  selector: 'app-breadcrumb',
  standalone: true,
  imports: [CommonModule],
  template: `
    <nav aria-label="breadcrumb" class="breadcrumb-nav">
      <ol class="breadcrumb">
        <li
          *ngFor="let item of items; let i = index; let last = last"
          class="breadcrumb-item"
          [class.active]="last"
          [class.disabled]="item.disabled"
        >
          <span
            *ngIf="item.icon && showIcons"
            class="breadcrumb-icon"
            [innerHTML]="item.icon"
          ></span>

          <a
            *ngIf="!last && !item.disabled && (item.url || clickable)"
            href="#"
            class="breadcrumb-link"
            (click)="onItemClick($event, item, i)"
            [attr.aria-current]="last ? 'page' : null"
          >
            {{ item.label }}
          </a>

          <span
            *ngIf="last || item.disabled || (!item.url && !clickable)"
            class="breadcrumb-text"
            [attr.aria-current]="last ? 'page' : null"
          >
            {{ item.label }}
          </span>

          <span
            *ngIf="!last && showSeparator"
            class="breadcrumb-separator"
            aria-hidden="true"
          >
            {{ separator }}
          </span>
        </li>
      </ol>
    </nav>
  `,
})
export class BreadcrumbComponent {
  @Input() items: BreadcrumbItem[] = [];
  @Input() separator: string = '/';
  @Input() showSeparator: boolean = true;
  @Input() showIcons: boolean = true;
  @Input() clickable: boolean = true;
  @Input() maxItems: number = 0;

  @Output() itemClick = new EventEmitter<{item: BreadcrumbItem, index: number}>();

  onItemClick(event: Event, item: BreadcrumbItem, index: number): void {
    event.preventDefault();

    if (item.disabled) {
      return;
    }

    this.itemClick.emit({ item, index });
  }

  get displayItems(): BreadcrumbItem[] {
    if (this.maxItems > 0 && this.items.length > this.maxItems) {
      const firstItem = this.items[0];
      const lastItems = this.items.slice(-2);
      return [
        firstItem,
        { label: '...', disabled: true },
        ...lastItems
      ];
    }
    return this.items;
  }
}
