import {Component, input} from '@angular/core';

@Component({
  selector: 'fp-image-album-grid-preview',
  imports: [],
  templateUrl: './image-album-grid-preview.html',
  styleUrl: './image-album-grid-preview.scss',
})
export class ImageAlbumGridPreview {
  imageUrls = input.required<string[]>();
}
