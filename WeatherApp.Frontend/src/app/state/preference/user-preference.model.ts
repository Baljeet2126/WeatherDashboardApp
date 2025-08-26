import { SunEvent } from '../../core/models/enums/sun-event.enum';
import { TemperatureUnit } from '../../core/models/enums/temperature-unit.enum';

export interface UserPreferenceStateModel {
  userId: string;
  temperatureUnit: TemperatureUnit;
  showSunrise: boolean;
}
