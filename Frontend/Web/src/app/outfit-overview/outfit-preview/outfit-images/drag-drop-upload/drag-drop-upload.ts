import {Directive, HostListener, output} from '@angular/core';

@Directive({
  selector: '[fpDragDropUpload]',
})
export class DragDropUpload {

  imageUrl = output<string>();

  @HostListener('drop', ['$event'])
  async onDrop(event: DragEvent) {
    event.preventDefault();
    const dataTransfer = event.dataTransfer;
    if(!dataTransfer)
      return;

    if( !dataTransfer.files || !dataTransfer.files.length ){
      return;
    }

    for (const file of dataTransfer.files) {
      if(file.type === 'image/jpeg' || file.type === 'image/png'){
        const url = URL.createObjectURL(file);
        this.imageUrl.emit(url);
      }
    }
  }

  @HostListener('dragover', ['$event'])
  onDragOver(event: DragEvent) {
    event.preventDefault();
  }
}
