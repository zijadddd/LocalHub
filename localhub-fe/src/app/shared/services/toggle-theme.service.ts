import { Injectable, signal } from '@angular/core';
import { AppTheme } from '../models/theme.model';

@Injectable({
  providedIn: 'root',
})
export class ToggleThemeService {
  private CLIENT_RENDER = typeof localStorage !== 'undefined';
  private LS_THEME = 'theme';
  private selectedTheme: AppTheme | undefined;
  private currentTheme = signal<AppTheme | undefined>(undefined);

  constructor() {
    this.selectedTheme = this.getStoredTheme();
    this.currentTheme.set(this.selectedTheme);
  }

  public getStoredTheme(): AppTheme | undefined {
    if (this.CLIENT_RENDER) {
      const theme = localStorage.getItem(this.LS_THEME) as AppTheme;
      return theme ? theme : undefined;
    }
    return undefined;
  }

  public setLightTheme() {
    this.currentTheme.set(AppTheme.LIGHT);
    this.setToLocalStorage(AppTheme.LIGHT);
    this.removeClassFromHtml('dark');
  }

  public setDarkTheme() {
    this.currentTheme.set(AppTheme.DARK);
    this.setToLocalStorage(AppTheme.DARK);
    this.addClassToHtml('dark');
  }

  private addClassToHtml(className: string) {
    if (this.CLIENT_RENDER) {
      this.removeClassFromHtml(className);
      document.documentElement.classList.add(className);
    }
  }

  private removeClassFromHtml(className: string) {
    if (this.CLIENT_RENDER) {
      document.documentElement.classList.remove(className);
    }
  }

  private setToLocalStorage(theme: AppTheme) {
    if (this.CLIENT_RENDER) {
      localStorage.setItem(this.LS_THEME, theme);
    }
  }
}
