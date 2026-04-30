import {Component, computed, inject, signal} from '@angular/core';
import {OutfitMetadataForm} from '../../components/outfit-creation/outfit-metadata-form/outfit-metadata.form';
import {FileHandler} from '../../services/file-handler/file-handler';
import {OutfitFile} from '../../../types/files.types';
import {LocalOutfit, Outfit, OutfitMetadataFormData} from '../../../types/outfit.types';
import {OutfitStore} from '../../stores/outfit-store/outfit.store';
import {OutfitApi} from '../../services/api/outfit-api/outfit-api';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';

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

  imagesMetadata = computed(() => this.fileHandler.files());

  protected readonly activeImage = signal<OutfitFile | undefined>(undefined);

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
    this.setActiveImage(this.imagesMetadata()[0]);
  }

  protected setActiveImage(metadata: OutfitFile) {
    this.activeImage.set(metadata);
  }

  protected handleSubmitted(outfitMetadata: OutfitMetadataFormData) {

    const outfit: LocalOutfit = {
      id: undefined,
      colors: outfitMetadata.colors.map(color => color.value),
      tags: outfitMetadata.tags.map(tag => tag.value),
      seasons: outfitMetadata.seasons,
      mood: outfitMetadata.mood,
      sport: outfitMetadata.outfitDestination.sport,
      images: this.fileHandler.files()
    }

    this.outfitApi.uploadOutfit(outfit)
      .subscribe(res => {
          this.outfitStore.addOutfit(res);
        }
      );
  }
}
