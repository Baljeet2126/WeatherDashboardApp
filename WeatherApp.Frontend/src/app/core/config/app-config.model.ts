import { TemperatureUnit } from "../models/enums/temperature-unit.enum";

export interface AppConfig {
  apiBaseUrl: string;      // Backend API base URL
  cacheExpirationMinutes: number; 
  defaultTemperatureUnit: TemperatureUnit
}