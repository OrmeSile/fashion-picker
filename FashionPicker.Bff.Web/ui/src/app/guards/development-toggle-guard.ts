import { CanActivateFn } from '@angular/router';
import {environment} from '../../environments/environment';

export const developmentToggleGuard: CanActivateFn = (_, __) => {
  return !environment.production;
};
