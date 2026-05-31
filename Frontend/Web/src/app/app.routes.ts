import {Routes} from '@angular/router';
import {MainPage} from './pages/main-page/main.page';
import {OutfitPreviewPage} from './pages/outfit-preview-page/outfit-preview.page';
import {ClothingManagementPage} from './pages/clothing-management-page/clothing-management.page';
import {TestBedPage} from './pages/test-bed-page/test-bed.page';
import {developmentToggleGuard} from './guards/development-toggle-guard';

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
  },
  {
    path: 'outfit/:id',
    component: OutfitPreviewPage
  },
  {
    path: 'testbed',
    component: TestBedPage,
    canMatch: [developmentToggleGuard]
  }
];
