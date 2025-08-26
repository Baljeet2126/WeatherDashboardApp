import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { WeatherRecord } from '../../core/models/weather-record.model';
import { MaterialModules } from '../../core/material.module';
import { NgIf } from '@angular/common';
import { WeatherState } from '../../state/weather/weather.state';
import { WeatherTrend } from '../../core/models/enums/weather-trend.enum';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-forecast',
  standalone: true,
  imports: [NgIf, RouterLink, ...MaterialModules],
  templateUrl: './forecast.component.html',
  styleUrls: ['./forecast.component.scss'],
})
export class ForecastComponent implements OnInit {
  cityId!: number;
  weatherForecast?: WeatherRecord;
  WeatherTrend = WeatherTrend;

  @Select(WeatherState.weatherRecords) weatherRecords$!: Observable<
    WeatherRecord[]
  >;

  constructor(
    private route: ActivatedRoute,
    private store: Store,
  ) {}

  ngOnInit(): void {
    this.cityId = +this.route.snapshot.paramMap.get('cityId')!;
    const allRecords = this.store.selectSnapshot(WeatherState.weatherRecords);
    this.weatherForecast = allRecords.find((r) => r.cityId === this.cityId);
  }
}
