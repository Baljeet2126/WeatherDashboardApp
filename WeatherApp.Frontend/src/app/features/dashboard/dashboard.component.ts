import { Component, OnInit } from '@angular/core';
import { AsyncPipe, NgForOf } from '@angular/common';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { WeatherCardComponent } from '../../shared/components/weather-card.component';
import { MaterialModules } from '../../core/material.module';
import { WeatherRecord } from '../../core/models/weather-record.model';
import { TemperatureUnit } from '../../core/models/enums/temperature-unit.enum';
import { LoadWeather } from '../../state/weather/weather.actions';
import { SaveUserPreference, SetSunEvent, SetTemperatureUnit } from '../../state/preference/user-preference.actions';
import { WeatherState } from '../../state/weather/weather.state';
import { UserPreferenceState } from '../../state/preference/user-preference.state';
import { SunEvent } from '../../core/models/enums/sun-event.enum';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [AsyncPipe, WeatherCardComponent, ...MaterialModules],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent implements OnInit {
    @Select(WeatherState.weatherRecords) weather$!: Observable<WeatherRecord[]>;
    @Select(UserPreferenceState.unit) unit$!: Observable<TemperatureUnit>;
    @Select(UserPreferenceState.showSunrise) showSunrise$!: Observable<boolean>;
    @Select(WeatherState.loading) loading$!: Observable<boolean>;

    constructor(private store: Store) {}

    ngOnInit(): void {
        const userId = 'demo-user-1';
        this.store.dispatch(new LoadWeather(userId));
    }

    onUnitChange(unit: TemperatureUnit) {
        const userId = 'demo-user-1'; 
        const showSunrise = this.store.selectSnapshot(UserPreferenceState.showSunrise);
        this.store.dispatch(new SaveUserPreference(userId, unit, showSunrise));
    }

    onToggleSunEvent(showSunrise: boolean) {
    const userId = 'demo-user-1'; 
    const unit = this.store.selectSnapshot(UserPreferenceState.unit);
    this.store.dispatch(new SaveUserPreference(userId, unit, showSunrise));
  }
}
