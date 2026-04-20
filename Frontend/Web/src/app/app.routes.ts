import {Routes} from '@angular/router';
import {MainPage} from './pages/main-page/main.page';
import {OutfitPreviewPage} from './pages/outfit-preview-page/outfit-preview.page';
import {ClothingManagementPage} from './pages/clothing-management-page/clothing-management.page';

export const routes: Routes = [
  {
    path: '',
    component: MainPage
  },
  {
    path: 'add',
    component: OutfitPreviewPage
  },
  {
    path: 'clothing',
    component: ClothingManagementPage
  }
];
