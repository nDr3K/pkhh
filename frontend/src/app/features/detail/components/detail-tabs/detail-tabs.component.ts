
import { Component, EventEmitter, Output } from '@angular/core';
import { NgIf } from "@angular/common";

@Component({
  selector: 'app-detail-tabs',
  templateUrl: './detail-tabs.component.html',
  styleUrls: ['./detail-tabs.component.scss'],
  imports: [
    NgIf
  ],
  standalone: true
})
export class DetailTabsComponent {
  @Output() tabChange = new EventEmitter<string>();
  activeTab = 'summary';

  setActiveTab(tab: string): void {
    this.activeTab = tab;
    this.tabChange.emit(tab);
  }
}
