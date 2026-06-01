import {Routes} from '@angular/router';
import {MainPage} from './pages/main-page/main.page';
import {ClothingManagementPage} from './pages/clothing-management-page/clothing-management.page';
import {TestBedPage} from './pages/test-bed-page/test-bed.page';
import {developmentToggleGuard} from './guards/development-toggle-guard';
import {OutfitEditPage} from './pages/outfit-preview-page/outfit-edit.page';

export const routes: Routes = [
  {
    path: '',
    component: MainPage
  },
  {
    path: 'add',
    component: OutfitEditPage
  },
  {
    path: 'clothing',
    component: ClothingManagementPage
  },
  {
    path: 'outfit/:id',
    component: OutfitEditPage
  },
  {
    path: 'testbed',
    component: TestBedPage,
    canMatch: [developmentToggleGuard]
  }
];
