import {Component, computed, inject, signal} from '@angular/core';
import {OutfitMetadataForm} from '../../components/outfit-creation/outfit-metadata-form/outfit-metadata.form';
import {FileHandler} from '../../services/file-handler/file-handler';
import {ImageFile} from '../../../types/files.types';
import {LocalOutfit, OutfitMetadataFormData} from '../../../types/outfit.types';
import {OutfitStore} from '../../stores/outfit-store/outfit.store';
import {OutfitApi} from '../../services/api/outfit-api/outfit-api';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';
import {ClothingStore} from '../../stores/clothing-store/clothing.store';
import {UUID} from '../../../types/shared.types';

@Component({
  selector: 'fp-outfit-preview-page',
  imports: [
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
  protected clothingStore = inject(ClothingStore);

  imagesMetadata = computed(() => this.fileHandler.files());

  protected readonly activeImage = signal<ImageFile | undefined>(undefined);

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
    this.setActiveImage(this.imagesMetadata()[0]);
  }

  protected setActiveImage(metadata: ImageFile) {
    this.activeImage.set(metadata);
  }

  protected handleSubmitted(outfitMetadata: OutfitMetadataFormData) {

    const clothingIds = Object.values(outfitMetadata.clothingGroups)
      .flatMap(x => Object.entries(x))
      .filter(([_, value]) => value)
      .map(([key, _]) => key) as UUID[];

    const outfit: LocalOutfit = {
      id: undefined,
      colors: outfitMetadata.colors.map(color => color.value),
      tags: outfitMetadata.tags.map(tag => tag.value),
      seasons: outfitMetadata.seasons,
      mood: outfitMetadata.mood,
      sport: outfitMetadata.outfitDestination.sport,
      images: this.fileHandler.files(),
      clothing: clothingIds
    }

    this.outfitApi.uploadOutfit(outfit)
      .subscribe(res => {
          this.outfitStore.addOutfit(res);
        }
      );
  }
}
