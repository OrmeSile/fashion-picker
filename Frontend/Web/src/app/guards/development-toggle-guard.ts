import { CanActivateFn } from '@angular/router';
import {environment} from '../../environments/environment';

export const developmentToggleGuard: CanActivateFn = (route, state) => {
  return !environment.production;
};
