import {Component, output, signal} from '@angular/core';
import {DragDropUpload} from '../drag-drop-upload/drag-drop-upload';

@Component({
  selector: 'fp-drop-upload',
  imports: [
    DragDropUpload,
  ],
  templateUrl: './drop-upload.html',
  styleUrl: './drop-upload.scss',
})
export class DropUpload {

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
