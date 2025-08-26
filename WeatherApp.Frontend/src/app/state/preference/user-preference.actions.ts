import { SunEvent } from "../../core/models/enums/sun-event.enum";
import { TemperatureUnit } from "../../core/models/enums/temperature-unit.enum";
import { UserPreference } from "../../core/models/user-preference.model";

export class SetTemperatureUnit {
  static readonly type = '[UserPreference] Set Unit';
  constructor(public unit: TemperatureUnit) {}
}

export class SetSunEvent {
  static readonly type = '[UserPreference] Set Sun Event';
   constructor(public sunEvent: SunEvent) {}
}

export class SaveUserPreference {
  static readonly type = '[UserPreference] Save';
  constructor(
    public userId: string,
    public unit: TemperatureUnit,
    public showSunrise: boolean
  ) {}
}