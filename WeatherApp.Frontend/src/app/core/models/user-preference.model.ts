import { TemperatureUnit } from './enums/temperature-unit.enum';

export interface UserPreference {
  userId: string;
  unit: TemperatureUnit;
  showSunrise: boolean;
}
