import {Directive, output} from '@angular/core';

@Directive({
  selector: '[fpDragDropUpload]',
  host: {
    '(drop)': 'onDrop($event)',
    '(dragover)': 'onDragOver($event)',
    '(dragenter)': 'onDragEnter($event)',
    '(dragleave)': 'onDragLeave($event)',
    '(click)': 'onClick($event)'
  }
})
export class DragDropUpload {

  dataTransfer = output<DataTransfer>();
  isDragOver = output<boolean>();

  onDrop(event: DragEvent) {
    event.preventDefault();
    const dataTransfer = event.dataTransfer;
    if(!dataTransfer)
      return;

    this.dataTransfer.emit(dataTransfer);
    this.isDragOver.emit(false);
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDragEnter(event: DragEvent){
    event.preventDefault();
    this.isDragOver.emit(true);
  }

  onDragLeave(event: DragEvent){
    event.preventDefault();
    this.isDragOver.emit(false);
  }

  protected onClick($event: PointerEvent) {

  }
}
