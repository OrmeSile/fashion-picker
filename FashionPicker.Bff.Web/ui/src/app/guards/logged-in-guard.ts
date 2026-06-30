import { CanActivateFn } from '@angular/router';
import {inject} from '@angular/core';
import {UserStore} from '../stores/user-store/user.store';


export const loggedInGuard: CanActivateFn = () => {
  const userStore = inject(UserStore);
  return userStore.isLoggedIn();
};
