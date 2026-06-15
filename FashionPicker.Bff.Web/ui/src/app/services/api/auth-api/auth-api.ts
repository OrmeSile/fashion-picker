import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../../../../types/User.types';

@Injectable({
  providedIn: 'root',
})
export class AuthApi {

  private readonly http = inject(HttpClient);

  getUserInformation(){
    return this.http.get<User>("/.auth/me");
  }
}
