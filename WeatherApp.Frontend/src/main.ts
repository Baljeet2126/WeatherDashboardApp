// src/main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { NgxsModule } from '@ngxs/store';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { MaterialModules } from './app/core/material.module';
import { UserPreferenceState } from './app/state/preference/user-preference.state';
import { WeatherState } from './app/state/weather/weather.state';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient(),
    importProvidersFrom(
      MaterialModules,
      NgxsModule.forRoot([UserPreferenceState, WeatherState]),
      NgxsStoragePluginModule.forRoot({ key: ['userPreference'] }),
      NgxsLoggerPluginModule.forRoot(),
    ),
  ],
});
