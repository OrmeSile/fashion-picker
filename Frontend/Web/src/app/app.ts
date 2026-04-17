import { Component, signal } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {OutfitPreview} from './outfit-overview/outfit-preview/outfit-preview';

@Component({
  selector: 'fp-root',
  imports: [RouterOutlet, OutfitPreview, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Web');
}
