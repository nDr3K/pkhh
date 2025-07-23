import { CanDeactivateFn } from '@angular/router';
import { EmulatorComponent } from '../emulator.component';

export const emulatorGuard: CanDeactivateFn<EmulatorComponent> = (component, currentRoute, currentState, nextState) => {
  if (component) {
    try {
      // Call component cleanup
      component.ngOnDestroy();

      // Force cleanup DOM + workers
      forceCleanupAllEmulatorElements();

    } catch (error) {
      console.warn('Error during route cleanup:', error);
    }
  }

  return true;
};

function forceCleanupAllEmulatorElements(): void {
  const selectors = [
    'iframe[src*="emulator"]',
    'iframe[src*="retroarch"]',
    'iframe[src*="wasm"]',
    'iframe[src*="data:"]',
    'canvas[class*="emulator"]',
    'canvas[id*="emulator"]',
    'div[class*="emulator"]',
    'div[id*="emulator"]',
    'audio[src*="emulator"]',
    'video[src*="emulator"]',
    '.emulator-wrapper',
    '.emulator-screen',
    '.emulator-canvas',
    '.retro-canvas',
    '#emulator-container',
    '[data-emulator]'
  ];

  selectors.forEach(selector => {
    const elements = document.querySelectorAll(selector);
    elements.forEach(element => {
      if (element.tagName === 'IFRAME') {
        const iframe = element as HTMLIFrameElement;
        try {
          iframe.contentWindow?.postMessage({ action: 'stop' }, '*');
          iframe.contentWindow?.postMessage({ action: 'pause' }, '*');
        } catch (error) {
          console.warn('Could not communicate with iframe:', error);
        }
        iframe.src = 'about:blank';
      }

      if (element.tagName === 'AUDIO' || element.tagName === 'VIDEO') {
        const media = element as HTMLMediaElement;
        if (!media.paused) {
          media.pause();
        }
        media.src = '';
        media.load();
      }

      if (element.tagName === 'CANVAS') {
        const canvas = element as HTMLCanvasElement;
        const ctx = canvas.getContext('2d');
        ctx?.clearRect(0, 0, canvas.width, canvas.height);
      }

      element.remove();
    });
  });

  cleanupWorkers();
}

function cleanupWorkers(): void {
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.getRegistrations()
      .then(registrations => {
        registrations.forEach(registration => {
          if (registration.scope.includes('emulator') ||
            registration.scope.includes('retroarch') ||
            registration.scope.includes('wasm')) {
            registration.unregister();
          }
        });
      })
      .catch(error => console.warn('Error cleaning up workers:', error));
  }
}
