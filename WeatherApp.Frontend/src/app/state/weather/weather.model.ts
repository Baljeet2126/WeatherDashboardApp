import { WeatherRecord } from "../../core/models/weather-record.model";

export interface WeatherStateModel {
   weatherRecords: WeatherRecord[];
  loading: boolean;
}
