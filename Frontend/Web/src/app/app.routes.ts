import {Routes} from '@angular/router';
import {MainPage} from './main-page/main-page';
import {OutfitPreview} from './outfit-overview/outfit-preview/outfit-preview';

export const routes: Routes = [
  {
    path: '',
    component: MainPage
  },
  {
    path: 'add',
    component: OutfitPreview
  }
];
