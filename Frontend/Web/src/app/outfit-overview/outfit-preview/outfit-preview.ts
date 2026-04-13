import {Component, computed, inject, input, signal} from '@angular/core';

import {UUID} from '../../../types/shared.types';
import {TechnicalOutfitMetadata} from '../../../types/files.types';
import {ImageAlbumGridPreview} from './image-album-grid-preview/image-album-grid-preview';
import {PreviewImage} from './image-album-grid-preview/preview-image/preview-image';
import {FileHandler} from './file-handler/file-handler';
import {DropUpload} from './drop-upload/drop-upload';

@Component({
  selector: 'fp-outfit-preview',
  imports: [
    ImageAlbumGridPreview,
    PreviewImage,
    DropUpload
  ],
  templateUrl: './outfit-preview.html',
  styleUrl: './outfit-preview.scss',
})
export class OutfitPreview {
  fileHandler = inject(FileHandler);

  imagesMetadata = computed(() => this.fileHandler.fileMetadata());

  protected readonly activeImage = signal<TechnicalOutfitMetadata | undefined>(undefined);

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
  }

  protected removeImage(id: UUID) {
    this.fileHandler.removeFile(id);
    if(this.activeImage()?.id === id){
      this.activeImage.set(undefined);
    }
  }

  protected setActiveImage(metadata: TechnicalOutfitMetadata) {
    this.activeImage.set(metadata);
  }
}
