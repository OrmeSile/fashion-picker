import {Component, computed, inject, input, signal} from '@angular/core';
import {PreviewImage} from '../../components/outfit-creation/preview-image/preview-image';
import {ImageAlbumGridPreview} from '../../components/outfit-creation/image-album-grid-preview/image-album-grid-preview';
import {DropZone} from '../../components/outfit-creation/drop-zone/drop-zone';
import {OutfitMetadataForm} from '../../components/outfit-creation/outfit-metadata-form/outfit-metadata.form';
import {FileHandler} from '../../services/file-handler/file-handler';
import {TechnicalOutfitMetadata} from '../../../types/files.types';
import {UUID} from '../../../types/shared.types';
import {Outfit, OutfitMetadataFormData} from '../../../types/outfit.types';
import {OutfitStore} from '../../stores/outfit-store/outfit.store';
import {OutfitApi} from '../../services/api/outfit-api/outfit-api';

@Component({
  selector: 'fp-outfit-preview-page',
  imports: [
    ImageAlbumGridPreview,
    PreviewImage,
    DropZone,
    OutfitMetadataForm
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

  protected readonly activeImage = signal<TechnicalOutfitMetadata | undefined>(undefined);

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

  protected setActiveImage(metadata: TechnicalOutfitMetadata) {
    this.activeImage.set(metadata);
  }

  protected handleSubmitted(outfitMetadata: OutfitMetadataFormData) {
    const newOutfit: Outfit = {
      id: undefined,
      colors: outfitMetadata.colors,
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
