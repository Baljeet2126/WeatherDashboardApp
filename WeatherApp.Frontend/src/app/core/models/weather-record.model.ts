import { WeatherTrend } from "./enums/weather-trend.enum";

export interface WeatherRecord {
  cityId: number;
  cityName: string;
  temperature: number;
  description: string;
  icon: string;
  sunriseTime: Date;
  sunsetTime: Date;
  trend: WeatherTrend;
}