import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {OutfitPreview} from './outfit-overview/outfit-preview/outfit-preview';

@Component({
  selector: 'fp-root',
  imports: [RouterOutlet, OutfitPreview],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Web');
}
