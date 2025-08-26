import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { WeatherRecord } from '../models/weather-record.model';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  constructor(private api: ApiService) {}

  getCurrentWeatherForAllCities(userId: string): Observable<WeatherRecord[]> {
    return this.api.get<WeatherRecord[]>('weather', userId);
  }

  getWeatherForecast(
    cityId: number,
    userId: string,
  ): Observable<WeatherRecord[]> {
    return this.api.get<WeatherRecord[]>(`weather/forecast/${cityId}`, userId);
  }
}
