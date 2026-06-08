import {Component, effect, inject} from '@angular/core';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {environment} from '../../../../environments/environment';
import Keycloak from 'keycloak-js';
import {KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs} from 'keycloak-angular';

@Component({
  selector: 'fp-navbar',
	imports: [
		RouterLink,
		RouterLinkActive
	],

  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
  host: {
    role: 'navigation'
  }
})

export class Navbar {
  protected readonly environment = environment;
  protected readonly keycloak = inject(Keycloak);
  protected readonly keycloakSignal = inject(KEYCLOAK_EVENT_SIGNAL);
  protected authenticated = false;

  constructor() {
    effect(() => {
      const keycloakEvent = this.keycloakSignal();

      if(keycloakEvent.type === KeycloakEventType.Ready){
        this.authenticated = typeEventArgs<ReadyArgs>(keycloakEvent.args);
      }

      if(keycloakEvent.type === KeycloakEventType.AuthLogout){
        this.authenticated = false;
      }
    });
  }

  protected async login() {
    await this.keycloak.login();
  }

  protected async logout() {
    await this.keycloak.logout();
  }
}
