import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { UserPreference } from '../models/user-preference.model';

@Injectable({
  providedIn: 'root',
})
export class UserPreferenceService {
  constructor(private api: ApiService) {}

  getUserPreference(userId: string): Observable<UserPreference> {
    return this.api.get<UserPreference>('weather/userpreference', userId);
  }

  saveUserPreference(preference: UserPreference): Observable<void> {
    return this.api.post<void>('weather/userpreference', preference);
  }
}
