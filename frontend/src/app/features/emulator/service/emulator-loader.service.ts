import { Injectable } from '@angular/core';

declare global {
  interface Window {
    EJS_player: string;
    EJS_gameUrl: string;
    EJS_core: string;
    EJS_pathtodata: string;
    EJS_startOnLoaded: boolean;
    EJS_loadStateOnStart: boolean;
    EJS_gameID: string;
    EJS_gameName: string;
    EJS_color: string;
    EJS_VirtualGamepadSettings: any;
    EJS_onGameStart: () => void;
    EJS_ready: () => void;
    EJS_onLoadState: () => void;
    EJS_onSaveState: () => void;
    EJS_onLoadSave: () => void;
    EJS_onSaveSave: (save: any) => void;
    EJS_emulator: any;
    EmulatorJS: any;
    EJS_paths: any;
    EJS_DEBUG_XX: boolean;
    EJS_language: string;
    EJS_disableAutoLang: boolean;
    EJS_adBlocked: (url: string, del: boolean) => void;
    EJS_biosUrl: string;
    EJS_AdUrl: string;
    EJS_AdMode: string;
    EJS_AdTimer: number;
    EJS_AdSize: string;
    EJS_alignStartButton: string;
    EJS_Buttons: any;
    EJS_volume: number;
    EJS_defaultControls: any;
    EJS_fullscreenOnLoaded: boolean;
    EJS_loadStateURL: string;
    EJS_CacheLimit: number;
    EJS_cheats: any;
    EJS_defaultOptions: any;
    EJS_gamePatchUrl: string;
    EJS_gameParentUrl: string;
    EJS_netplayServer: string;
    EJS_backgroundImage: string;
    EJS_backgroundBlur: boolean;
    EJS_backgroundColor: string;
    EJS_controlScheme: any;
    EJS_threads: number;
    EJS_disableCue: boolean;
    EJS_startButtonName: string;
    EJS_softLoad: boolean;
    EJS_screenCapture: boolean;
    EJS_externalFiles: any;
    EJS_dontExtractBIOS: boolean;
    EJS_disableDatabases: boolean;
    EJS_disableLocalStorage: boolean;
    EJS_forceLegacyCores: boolean;
    EJS_noAutoFocus: boolean;
    EJS_videoRotation: number;
    EJS_hideSettings: boolean;
    EJS_SHADERS: any;
    EJS_shaders: any;
  }
}

export interface EmulatorConfig {
  player: string;
  gameUrl: string;
  core: string;
  pathtodata?: string;
  startOnLoaded?: boolean;
  loadStateOnStart?: boolean;
  gameID?: string;
  gameName?: string;
  color?: string;
  VirtualGamepadSettings?: any;
  onGameStart?: () => void;
  onReady?: () => void;
  onLoadState?: () => void;
  onSaveState?: () => void;
  onLoadSave?: () => void;
  onSaveSave?: (save: any) => void;
  biosUrl?: string;
  volume?: number;
  defaultControls?: any;
  fullscreenOnLoaded?: boolean;
  backgroundImage?: string;
  backgroundColor?: string;
  language?: string;
  debug?: boolean;
  buttonOpts?: Record<string, any>;
}

@Injectable({
  providedIn: 'root'
})
export class EmulatorLoaderService {
  private scriptsLoaded = false;
  private loadingPromise: Promise<void> | null = null;
  private currentEmulator: any = null;

  constructor() {}

  /**
   * Initialize EmulatorJS with the given configuration
   */
  async initializeEmulator(config: EmulatorConfig): Promise<any> {
    // Set global configuration
    this.setGlobalConfig(config);

    // Load scripts if not already loaded
    if (!this.scriptsLoaded) {
      if (!this.loadingPromise) {
        this.loadingPromise = this.loadEmulatorScripts();
      }
      await this.loadingPromise;
    }

    // Create and return a new emulator instance
    return this.createEmulatorInstance(config);
  }

  /**
   * Cleanup current emulator instance
   */
  cleanup(): void {
    if (this.currentEmulator) {
      try {
        // Try multiple cleanup methods
        if (typeof this.currentEmulator.destroy === 'function') {
          this.currentEmulator.destroy();
        }
        if (typeof this.currentEmulator.pause === 'function') {
          this.currentEmulator.pause();
        }
        if (typeof this.currentEmulator.stop === 'function') {
          this.currentEmulator.stop();
        }

        // Force cleanup of any iframe or canvas elements
        this.forceCleanupEmulatorElements();

      } catch (error) {
        console.warn('Error destroying emulator:', error);
        // Force cleanup even if destroy fails
        this.forceCleanupEmulatorElements();
      }
      this.currentEmulator = null;
    }

    // Clean up global variables
    this.cleanupGlobalVariables();
  }

  /**
   * Force reload of scripts (useful for debugging)
   */
  async forceReload(): Promise<void> {
    this.cleanup();
    this.scriptsLoaded = false;
    this.loadingPromise = null;

    // Remove existing scripts
    this.removeExistingScripts();

    // Wait a bit before reloading
    await new Promise(resolve => setTimeout(resolve, 100));
  }

  private setGlobalConfig(config: EmulatorConfig): void {
    window.EJS_player = config.player;
    window.EJS_gameUrl = config.gameUrl;
    window.EJS_core = config.core;
    window.EJS_pathtodata = config.pathtodata || 'https://cdn.emulatorjs.org/stable/data/';
    window.EJS_startOnLoaded = config.startOnLoaded ?? true;
    window.EJS_loadStateOnStart = config.loadStateOnStart ?? false;
    window.EJS_gameID = config.gameID || this.generateGameID();
    window.EJS_gameName = config.gameName || 'Game';
    window.EJS_color = config.color || '#74b9ff';
    window.EJS_VirtualGamepadSettings = config.VirtualGamepadSettings || {};

    // Set callback functions
    if (config.onGameStart) window.EJS_onGameStart = config.onGameStart;
    if (config.onReady) window.EJS_ready = config.onReady;
    if (config.onLoadState) window.EJS_onLoadState = config.onLoadState;
    if (config.onSaveState) window.EJS_onSaveState = config.onSaveState;
    if (config.onLoadSave) window.EJS_onLoadSave = config.onLoadSave;
    if (config.onSaveSave) window.EJS_onSaveSave = config.onSaveSave;

    // Set optional config properties
    if (config.biosUrl) window.EJS_biosUrl = config.biosUrl;
    if (config.volume !== undefined) window.EJS_volume = config.volume;
    if (config.defaultControls) window.EJS_defaultControls = config.defaultControls;
    if (config.fullscreenOnLoaded !== undefined) window.EJS_fullscreenOnLoaded = config.fullscreenOnLoaded;
    if (config.backgroundImage) window.EJS_backgroundImage = config.backgroundImage;
    if (config.backgroundColor) window.EJS_backgroundColor = config.backgroundColor;
    if (config.language) window.EJS_language = config.language;
    if (config.debug) window.EJS_DEBUG_XX = config.debug;
    if (config.buttonOpts) window.EJS_Buttons = config.buttonOpts;
  }

  private async loadEmulatorScripts(): Promise<void> {
    const scripts = [
      "emulator.js",
      "nipplejs.js",
      "shaders.js",
      "storage.js",
      "gamepad.js",
      "GameManager.js",
      "socket.io.min.js",
      "compression.js"
    ];

    const scriptPath = window.EJS_pathtodata.endsWith('/') ? window.EJS_pathtodata : window.EJS_pathtodata + '/';

    try {
      if (window.EJS_DEBUG_XX) {
        // Load individual scripts in debug mode
        for (const script of scripts) {
          await this.loadScript(scriptPath + 'src/' + script);
        }
        await this.loadStyle(scriptPath + 'emulator.css');
      } else {
        // Load minified versions
        await this.loadScript(scriptPath + 'emulator.min.js');
        await this.loadStyle(scriptPath + 'emulator.min.css');
      }

      this.scriptsLoaded = true;
    } catch (error) {
      console.error('Failed to load EmulatorJS scripts:', error);
      // Try loading individual scripts as fallback
      for (const script of scripts) {
        await this.loadScript(scriptPath + 'src/' + script);
      }
      await this.loadStyle(scriptPath + 'emulator.css');
      this.scriptsLoaded = true;
    }
  }

  private loadScript(src: string): Promise<void> {
    return new Promise((resolve, reject) => {
      // Check if the script already exists
      if (document.querySelector(`script[src="${src}"]`)) {
        resolve();
        return;
      }

      const script = document.createElement('script');
      script.src = src;
      script.onload = () => resolve();
      script.onerror = () => reject(new Error(`Failed to load script: ${src}`));
      document.head.appendChild(script);
    });
  }

  private loadStyle(href: string): Promise<void> {
    return new Promise((resolve, reject) => {
      // Check if style already exists
      if (document.querySelector(`link[href="${href}"]`)) {
        resolve();
        return;
      }

      const link = document.createElement('link');
      link.rel = 'stylesheet';
      link.href = href;
      link.onload = () => resolve();
      link.onerror = () => reject(new Error(`Failed to load style: ${href}`));
      document.head.appendChild(link);
    });
  }

  private async createEmulatorInstance(config: EmulatorConfig): Promise<any> {
    // Build a config object similar to the original loader
    const emulatorConfig: any = {
      gameUrl: window.EJS_gameUrl,
      dataPath: window.EJS_pathtodata,
      system: window.EJS_core,
      biosUrl: window.EJS_biosUrl,
      gameName: window.EJS_gameName,
      color: window.EJS_color,
      VirtualGamepadSettings: window.EJS_VirtualGamepadSettings,
      startOnLoad: window.EJS_startOnLoaded,
      fullscreenOnLoad: window.EJS_fullscreenOnLoaded,
      gameId: window.EJS_gameID,
      backgroundImg: window.EJS_backgroundImage,
      backgroundColor: window.EJS_backgroundColor,
      buttonOpts: window.EJS_Buttons,
    };

    // Handle language loading
    if (window.EJS_language || window.EJS_disableAutoLang) {
      const language = window.EJS_language || this.getSystemLanguage();
      if (language && language !== 'en-US') {
        try {
          const langPath = window.EJS_pathtodata + 'localization/' + language + '.json';
          const response = await fetch(langPath);
          const langJson = await response.json();
          emulatorConfig.language = language;
          emulatorConfig.langJson = langJson;
        } catch (error) {
          console.error('Missing language', language, '!!');
        }
      }
    }

    // Create emulator instance
    if (!window.EmulatorJS) {
      throw new Error('EmulatorJS not available. Scripts may not have loaded correctly.');
    }

    this.currentEmulator = new window.EmulatorJS(config.player, emulatorConfig);
    window.EJS_emulator = this.currentEmulator;

    // Set up an ad blocking function
    window.EJS_adBlocked = (url: string, del: boolean) => this.currentEmulator.adBlocked(url, del);

    // Set up event listeners
    if (window.EJS_ready) {
      this.currentEmulator.on('ready', window.EJS_ready);
    }
    if (window.EJS_onGameStart) {
      this.currentEmulator.on('start', window.EJS_onGameStart);
    }
    if (window.EJS_onLoadState) {
      this.currentEmulator.on('loadState', window.EJS_onLoadState);
    }
    if (window.EJS_onSaveState) {
      this.currentEmulator.on('saveState', window.EJS_onSaveState);
    }
    if (window.EJS_onLoadSave) {
      this.currentEmulator.on('loadSave', window.EJS_onLoadSave);
    }
    if (window.EJS_onSaveSave) {
      this.currentEmulator.on('saveSave', window.EJS_onSaveSave);
    }

    return this.currentEmulator;
  }

  private getSystemLanguage(): string {
    return navigator.language;
  }

  private generateGameID(): string {
    return `emulator_${Date.now()}_${Math.random().toString(36).substring(2, 11)}`;
  }

  private cleanupGlobalVariables(): void {
    const globalVars = [
      'EJS_player', 'EJS_gameUrl', 'EJS_core', 'EJS_pathtodata',
      'EJS_startOnLoaded', 'EJS_loadStateOnStart', 'EJS_gameID',
      'EJS_gameName', 'EJS_color', 'EJS_VirtualGamepadSettings',
      'EJS_onGameStart', 'EJS_ready', 'EJS_onLoadState', 'EJS_onSaveState',
      'EJS_onLoadSave', 'EJS_onSaveSave', 'EJS_emulator', 'EJS_adBlocked',
      'EJS_biosUrl', 'EJS_volume', 'EJS_defaultControls', 'EJS_fullscreenOnLoaded',
      'EJS_backgroundImage', 'EJS_backgroundColor', 'EJS_language', 'EJS_DEBUG_XX', 'EJS_Buttons'
    ];

    globalVars.forEach(varName => {
      delete (window as any)[varName];
    });
  }

  private removeExistingScripts(): void {
    // Remove EmulatorJS scripts
    const scripts = document.querySelectorAll('script[src*="emulator"]');
    scripts.forEach(script => script.remove());

    // Remove EmulatorJS styles
    const styles = document.querySelectorAll('link[href*="emulator"]');
    styles.forEach(style => style.remove());
  }

  /**
   * Force cleanup of emulator DOM elements
   */
  private forceCleanupEmulatorElements(): void {
    // Find and remove all emulator-related elements
    const selectors = [
      'iframe[src*="emulator"]',
      'iframe[src*="retroarch"]',
      'iframe[src*="wasm"]',
      'canvas[class*="emulator"]',
      'canvas[id*="emulator"]',
      'div[class*="emulator-container"]',
      'div[id*="emulator"]',
      '.emulator-wrapper',
      '.emulator-screen',
      '.emulator-canvas'
    ];

    selectors.forEach(selector => {
      const elements = document.querySelectorAll(selector);
      elements.forEach(element => {
        // If it's an iframe, try to pause/stop it first
        if (element.tagName === 'IFRAME') {
          try {
            const iframe = element as HTMLIFrameElement;
            if (iframe.contentWindow) {
              // Try to pause the iframe content
              iframe.contentWindow.postMessage({ action: 'pause' }, '*');
            }
          } catch (error) {
            console.warn('Could not pause iframe:', error);
          }
        }

        // Remove the element
        element.remove();
      });
    });

    // Also check for any audio contexts or media elements
    this.cleanupAudioElements();
  }

  /**
   * Clean up audio elements and contexts
   */
  private cleanupAudioElements(): void {
    try {
      // Find and pause all audio/video elements
      const mediaElements = document.querySelectorAll('audio, video');
      mediaElements.forEach(element => {
        const media = element as HTMLMediaElement;
        if (!media.paused) {
          media.pause();
        }
        // Clear the source to stop loading
        media.src = '';
        media.load();
      });

      // Try to suspend any running audio contexts
      if (window.AudioContext || (window as any).webkitAudioContext) {
        console.log('Attempting to clean up audio contexts...');
      }
    } catch (error) {
      console.warn('Error cleaning up audio elements:', error);
    }
  }
}
