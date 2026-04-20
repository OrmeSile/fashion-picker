import { Component } from '@angular/core';
import {DragDroppable} from '../../shared/directives/drag-droppable.directive';

@Component({
  selector: 'fp-clothing-management-page',
  imports: [
    DragDroppable
  ],
  templateUrl: './clothing-management.page.html',
  styleUrl: './clothing-management.page.scss',
})
export class ClothingManagementPage {
  protected handleDataTransfer(dataTransfer: DataTransfer) {
    console.log(dataTransfer);
  }
}
