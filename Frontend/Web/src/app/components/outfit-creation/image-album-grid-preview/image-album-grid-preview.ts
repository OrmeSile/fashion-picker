import {Component, output} from '@angular/core';

@Component({
  selector: 'fp-image-album-grid-preview',
  imports: [

  ],
  templateUrl: './image-album-grid-preview.html',
  styleUrl: './image-album-grid-preview.scss',
})
export class ImageAlbumGridPreview {
  removeClick = output<string>();
}
