import {Routes} from '@angular/router';
import {developmentToggleGuard} from './guards/development-toggle-guard';
import {MainPage} from './pages/main.page/main.page';
import {OutfitEditPage} from './pages/outfit-edit.page/outfit-edit.page';
import {ClothingManagementPage} from './pages/clothing-management.page/clothing-management.page';
import {TestBedPage} from './pages/test-bed.page/test-bed.page';
import {NotFoundPage} from './pages/not-found.page/not-found.page';
import {loggedInGuard} from './guards/logged-in-guard';

export const routes: Routes = [
  {path: '', component: MainPage},
  {path: 'add', component: OutfitEditPage, canMatch: [loggedInGuard]},
  {path: 'clothing', component: ClothingManagementPage, canMatch: [loggedInGuard]},
  {path: 'outfit/:id', component: OutfitEditPage, canMatch: [loggedInGuard]},
  {path: 'testbed', component: TestBedPage, canMatch: [developmentToggleGuard]},
  {path: "**", component: NotFoundPage}
];
