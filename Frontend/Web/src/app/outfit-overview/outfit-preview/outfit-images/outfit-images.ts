import {Component, signal} from '@angular/core';
import {DragDropUpload} from './drag-drop-upload/drag-drop-upload';
import {ImageAlbumGridPreview} from './image-album-grid-preview/image-album-grid-preview/image-album-grid-preview';

@Component({
  selector: 'fp-outfit-images',
  imports: [
    DragDropUpload,
    ImageAlbumGridPreview
  ],
  templateUrl: './outfit-images.html',
  styleUrl: './outfit-images.scss',
})
export class OutfitImages {

  imageUrls = signal<string[]>([]);

  protected setImage($event: string) {
    this.imageUrls.update(imageUrls => [...imageUrls, $event]);
  }
}
