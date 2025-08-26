import { TemperatureUnit } from "../app/core/models/enums/temperature-unit.enum";

export const environment = {
  production: false,
  apiBaseUrl: 'https://localhost:7085/api/v1/WeatherDashboard/', // backend running locally
  openWeatherApiKey: 'f00c38e0279b7bc85480c3fe775d518c',
  cacheExpirationMinutes: 10,
  defaultTemperatureUnit: TemperatureUnit.Celsius
};