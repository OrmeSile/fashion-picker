import {Component, computed, inject} from '@angular/core';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {environment} from '../../../../environments/environment';
import {UserStore} from '../../../stores/user-store/user.store';

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
  private readonly userStore = inject(UserStore);
  protected loggedIn = computed(() => this.userStore.isLoggedIn());
}
