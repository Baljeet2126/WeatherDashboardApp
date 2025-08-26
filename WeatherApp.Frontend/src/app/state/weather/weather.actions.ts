export class LoadWeather {
  static readonly type = '[Weather] Load';
  constructor(public userId: string) {}
}

export class LoadWeatherSuccess {
  static readonly type = '[Weather] Load Success';
  constructor(public payload: any) {}
}

export class LoadWeatherFail {
  static readonly type = '[Weather] Load Fail';
  constructor(public error: any) {}
}
