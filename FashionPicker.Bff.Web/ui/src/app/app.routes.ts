import {Routes} from '@angular/router';
import {developmentToggleGuard} from './guards/development-toggle-guard';
import {MainPage} from './pages/main.page/main.page';
import {OutfitEditPage} from './pages/outfit-preview.page/outfit-edit.page';
import {ClothingManagementPage} from './pages/clothing-management.page/clothing-management.page';
import {TestBedPage} from './pages/test-bed.page/test-bed.page';
import {NotFoundPage} from './pages/not-found.page/not-found.page';

export const routes: Routes = [
  {path: '', component: MainPage},
  {path: 'add', component: OutfitEditPage},
  {path: 'clothing', component: ClothingManagementPage},
  {path: 'outfit/:id', component: OutfitEditPage},
  {path: 'testbed', component: TestBedPage, canMatch: [developmentToggleGuard]},
  {path: "**", component: NotFoundPage}
];
