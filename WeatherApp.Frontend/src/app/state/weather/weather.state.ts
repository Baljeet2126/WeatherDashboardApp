import { State, Action, StateContext, Selector } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { WeatherService } from '../../core/services/weather.service';
import { WeatherStateModel } from './weather.model';
import { LoadWeather, LoadWeatherSuccess, LoadWeatherFail } from './weather.actions';
import { WeatherRecord } from '../../core/models/weather-record.model';
import { tap } from 'rxjs/operators';

@State<WeatherStateModel>({
  name: 'weather',
  defaults: {
    weatherRecords: [],
    loading: false
  }
})
@Injectable()
export class WeatherState {
  constructor(private weatherService: WeatherService) {}

    @Selector()
    static weatherRecords(state: WeatherStateModel) {
    return state.weatherRecords;
  }

  @Selector()
  static loading(state: WeatherStateModel) {
    return state.loading;
  }

  @Action(LoadWeather)
loadWeather(ctx: StateContext<WeatherStateModel>, action: LoadWeather) {
  ctx.patchState({ loading: true });

  return this.weatherService.getCurrentWeatherForAllCities(action.userId).pipe(
    tap({
      next: (data :WeatherRecord[]) => {
        ctx.dispatch(new LoadWeatherSuccess(data));
      },
      error: (err: any) => {
        ctx.dispatch(new LoadWeatherFail(err));
      }
    })
  );
}


  @Action(LoadWeatherSuccess)
  loadWeatherSuccess(ctx: StateContext<WeatherStateModel>, action: LoadWeatherSuccess) {
    ctx.patchState({
      weatherRecords: action.payload,
      loading: false
    });
  }

  @Action(LoadWeatherFail)
  loadWeatherFail(ctx: StateContext<WeatherStateModel>, action: LoadWeatherFail) {
    ctx.patchState({ loading: false });
    console.error('Weather load failed', action.error);
  }
}
