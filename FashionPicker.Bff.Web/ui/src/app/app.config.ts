import {ApplicationConfig, CSP_NONCE, provideBrowserGlobalErrorListeners} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {provideHttpClient, withFetch, withInterceptors} from '@angular/common/http';
import {secureApiInterceptor} from './secure-api.interceptor';

const nonce = (
  document.querySelector('meta[name="CSP_NONCE"]') as HTMLMetaElement
)?.content;

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withFetch(), withInterceptors([secureApiInterceptor])),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    {provide: CSP_NONCE, useValue: nonce},
  ]
};
