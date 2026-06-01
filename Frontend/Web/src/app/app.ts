import { Component, signal } from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {environment} from '../environments/environment';
import {Navbar} from './components/shared/navbar/navbar';

@Component({
  selector: 'fp-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Web');
  protected readonly environment = environment;
}
