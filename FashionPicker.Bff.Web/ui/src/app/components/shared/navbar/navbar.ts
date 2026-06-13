import {Component } from '@angular/core';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {environment} from '../../../../environments/environment';

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

  protected async me(){
    const res = await fetch("/me");
    const json = await res.json();
    console.log(json);
  }
}
