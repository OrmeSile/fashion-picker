import {Component, computed, inject, signal} from '@angular/core';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';
import {ImageAlbumGridPreview} from '../../components/shared/image-album-grid-preview/image-album-grid-preview';
import {FileHandler} from '../../services/file-handler/file-handler';
import {PreviewImage} from '../../components/shared/preview-image/preview-image';
import {OutfitFile} from '../../../types/files.types';
import {UUID} from '../../../types/shared.types';

@Component({
  selector: 'fp-clothing-management-page',
  imports: [
    DropZone,
    ImageAlbumGridPreview,
    PreviewImage
  ],
  providers: [FileHandler],
  templateUrl: './clothing-management.page.html',
  styleUrl: './clothing-management.page.scss',
})
export class ClothingManagementPage {

  fileHandler = inject(FileHandler);

  files = computed(() => this.fileHandler.files());
  activeImage = signal<OutfitFile | undefined>(undefined);

  protected removeImage(id: UUID) {
    this.fileHandler.removeFile(id);
    if (this.activeImage()?.id === id) {
      this.activeImage.set(this.files()[0]);
    }
  }

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
    this.setActiveImage(this.files()[0]);
  }

  protected setActiveImage(outfitFile: OutfitFile) {
    this.activeImage.set(outfitFile);
  }
}
