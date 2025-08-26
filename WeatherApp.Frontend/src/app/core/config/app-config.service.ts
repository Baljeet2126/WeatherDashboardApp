// src/app/core/config/app-config.service.ts
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { AppConfig } from './app-config.model';
import { TemperatureUnit } from '../models/enums/temperature-unit.enum';

@Injectable({
  providedIn: 'root',
})
export class AppConfigService {
  private readonly config: AppConfig = {
    apiBaseUrl: environment.apiBaseUrl,
    cacheExpirationMinutes: environment.cacheExpirationMinutes ?? 10, // fallback to 10min
    defaultTemperatureUnit: environment.defaultTemperatureUnit,
  };

  getConfig(): AppConfig {
    return this.config;
  }

  get apiBaseUrl(): string {
    return this.config.apiBaseUrl;
  }

  get cacheExpirationMinutes(): number {
    return this.config.cacheExpirationMinutes;
  }

  get defaultTemperatureUnit(): TemperatureUnit {
    return this.config.defaultTemperatureUnit;
  }
}
