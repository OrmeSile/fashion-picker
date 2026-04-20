import {Component, output, signal} from '@angular/core';
import {DragDroppable} from '../../../shared/directives/drag-droppable.directive';

@Component({
  selector: 'fp-drop-zone',
  imports: [
    DragDroppable,
  ],
  templateUrl: './drop-zone.html',
  styleUrl: './drop-zone.scss',
})
export class DropZone {

  files = output<FileList>();
  isDragOver = signal<boolean>(false);
  protected async handleDataTransfer(dataTransfer: DataTransfer) {

    if (!dataTransfer.files || !dataTransfer.files.length) {
      return;
    }
    this.files.emit(dataTransfer.files);
  }

  protected handleFileChange(fileChangeEvent: Event) {
    const files = (fileChangeEvent.target as HTMLInputElement).files;
    if(files) {
      this.files.emit(files);
    }
  }
}
