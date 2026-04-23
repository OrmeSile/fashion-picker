import {Component, computed, inject, signal} from '@angular/core';
import {OutfitMetadataForm} from '../../components/outfit-creation/outfit-metadata-form/outfit-metadata.form';
import {FileHandler} from '../../services/file-handler/file-handler';
import {OutfitFile, TechnicalOutfitMetadata} from '../../../types/files.types';
import {UUID} from '../../../types/shared.types';
import {Outfit, OutfitMetadataFormData} from '../../../types/outfit.types';
import {OutfitStore} from '../../stores/outfit-store/outfit.store';
import {OutfitApi} from '../../services/api/outfit-api/outfit-api';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';
import {ImageAlbumGridPreview} from '../../components/shared/image-album-grid-preview/image-album-grid-preview';
import {PreviewImage} from '../../components/shared/preview-image/preview-image';

@Component({
  selector: 'fp-outfit-preview-page',
  imports: [
    ImageAlbumGridPreview,
    PreviewImage,
    DropZone,
    OutfitMetadataForm,
    DropZone
  ],
  providers: [FileHandler],
  templateUrl: './outfit-preview.page.html',
  styleUrl: './outfit-preview.page.scss',
})
export class OutfitPreviewPage {

  private outfitStore = inject(OutfitStore);
  fileHandler = inject(FileHandler);
  private outfitApi = inject(OutfitApi);

  imagesMetadata = computed(() => this.fileHandler.files());

  protected readonly activeImage = signal<OutfitFile | undefined>(undefined);

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
    this.setActiveImage(this.imagesMetadata()[0]);
  }

  protected removeImage(id: UUID) {
    this.fileHandler.removeFile(id);
    if (this.activeImage()?.id === id) {
      this.activeImage.set(undefined);
    }
  }

  protected setActiveImage(metadata: OutfitFile) {
    this.activeImage.set(metadata);
  }

  protected handleSubmitted(outfitMetadata: OutfitMetadataFormData) {
    const newOutfit: Outfit = {
      id: undefined,
      colors: outfitMetadata.colors.map(color => color.value),
      tags: outfitMetadata.tags.map(tag => tag.value),
      seasons: outfitMetadata.seasons,
      imageUrls: this.fileHandler.files().map(file => file.fileUrl),
      images: this.fileHandler.files()
    }
    this.outfitApi.uploadClothing(this.fileHandler.files().map(outfitFile => outfitFile.file))
      .subscribe(res => console.log(res));
    this.outfitStore.addOutfit(newOutfit);
  }
}
