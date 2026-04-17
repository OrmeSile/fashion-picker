import {Component, inject} from '@angular/core';
import {OutfitStore} from '../outfit-overview/outfit-store/outfit-store';

@Component({
  selector: 'fp-main-page',
  imports: [],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {
  outfitStore = inject(OutfitStore);
}
