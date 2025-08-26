import { State, Action, StateContext, Selector } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { UserPreferenceStateModel } from './user-preference.model';
import { SaveUserPreference, SetSunEvent, SetTemperatureUnit } from './user-preference.actions';
import { TemperatureUnit } from '../../core/models/enums/temperature-unit.enum';
import { UserPreferenceService } from '../../core/services/user-preference.service';


@State<UserPreferenceStateModel>({
  name: 'userPreference',
  defaults: {
    userId:'all',
    temperatureUnit: TemperatureUnit.Celsius,
    showSunrise: true
  }
})
@Injectable()
export class UserPreferenceState {
  constructor(private userPreferenceService: UserPreferenceService) {}

  @Selector()
  static unit(state: UserPreferenceStateModel) {
    return state.temperatureUnit;
  }

  @Selector()
  static showSunrise(state: UserPreferenceStateModel) {
    return state.showSunrise;
  }

@Action(SaveUserPreference)
savePreference(ctx: StateContext<UserPreferenceStateModel>, action: SaveUserPreference) {
  ctx.patchState({
    temperatureUnit: action.unit,
    showSunrise: action.showSunrise,
    userId: action.userId
  });

  return this.userPreferenceService.saveUserPreference({
      unit: action.unit,
      showSunrise: action.showSunrise,
      userId: action.userId
  });
}
}
