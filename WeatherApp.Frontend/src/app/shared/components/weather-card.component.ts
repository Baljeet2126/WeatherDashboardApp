import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TemperatureUnit } from '../../core/models/enums/temperature-unit.enum';
import { WeatherRecord } from '../../core/models/weather-record.model';
import { WeatherTrend } from '../../core/models/enums/weather-trend.enum';
import { MaterialModules } from '../../core/material.module';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-weather-card',
  standalone: true,
  templateUrl: './weather-card.component.html',
  styleUrls: ['./weather-card.component.scss'],
  imports: [CommonModule, RouterLink, MaterialModules],
})
export class WeatherCardComponent {
  @Input() weather!: WeatherRecord;
  @Input() unit: TemperatureUnit = TemperatureUnit.Celsius;
  @Input() showSunrise: boolean = true;

  get temperatureDisplay(): string {
    return this.unit === TemperatureUnit.Celsius
      ? `${this.weather.temperature} °C`
      : `${this.weather.temperature} °F`;
  }

  get sunEventTime(): string {
    const timestamp = this.showSunrise
      ? this.weather.sunriseTime
      : this.weather.sunsetTime;
    return new Date(timestamp).toLocaleTimeString([], {
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  get trendIcon(): string {
    switch (this.weather.trend.toLocaleLowerCase()) {
      case WeatherTrend.Rising:
        return 'arrow_upward';
      case WeatherTrend.Falling:
        return 'arrow_downward';
      default:
        return 'horizontal_rule';
    }
  }

  get trendTooltip(): string {
    switch (this.weather.trend.toLocaleLowerCase()) {
      case WeatherTrend.Rising:
        return 'Rising in last 3 hours';
      case WeatherTrend.Falling:
        return 'Falling in last 3 hours';
      default:
        return 'Stable';
    }
  }
  get iconUrl(): string {
    return `https://openweathermap.org/img/wn/${this.weather.icon}@2x.png`;
  }
}
