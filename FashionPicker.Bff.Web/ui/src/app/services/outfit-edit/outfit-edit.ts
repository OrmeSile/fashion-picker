import {computed, inject, Injectable, linkedSignal, signal} from '@angular/core';
import {UUID} from '../../../types/shared.types';
import {OutfitStore} from '../../stores/outfit-store/outfit.store';
import {FileHandler} from '../file-handler/file-handler';
import {OutfitApi} from '../api/outfit-api/outfit-api';
import {ClothingStore} from '../../stores/clothing-store/clothing.store';
import {ImageFile} from '../../../types/files.types';
import {LocalOutfit, Outfit, OutfitMetadataFormData} from '../../../types/outfit.types';
import {map, tap} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OutfitEdit {

  private outfitApi = inject(OutfitApi);
  private outfitStore = inject(OutfitStore);

  public fileHandler = inject(FileHandler);
  public clothingStore = inject(ClothingStore);

  outfitId = signal<UUID | undefined>(undefined);

  imagesMetadata = computed(() => this.fileHandler.files());
  outfit = signal<Outfit | undefined>(undefined);

  isInEditMode = linkedSignal({source: this.outfit, computation: (outfit) => outfit !== undefined})

  public readonly activeImage = signal<ImageFile | undefined>(undefined);

  setOutfitId(outfitId: UUID) {
    this.outfitId.set(outfitId);
    let outfit = this.outfitStore.state()
      .find(outfit => outfit.id === outfitId);
    if (outfit)
      this.outfit.set(outfit);

    this.outfitApi.getOutfitById(outfitId)
      .subscribe(outfit => {
        this.outfitStore.dispatch({type: 'ADD_OUTFIT', payload: outfit});
        this.outfit.set(this.outfitStore.state()
          .find(x => x.id === outfitId));
      });
  }

  addImages(fileList: FileList) {
    this.fileHandler.addFiles(fileList);
    this.activeImage.set(this.imagesMetadata()[0]);
  }


  saveChanges(outfitMetadata: OutfitMetadataFormData) {
    const outfit = this.prepareData(outfitMetadata, this.outfitId());

    if (this.isInEditMode()) {
      return this.outfitApi.editOutfit(outfit)
        .pipe(tap(outfit => {
          this.outfitStore.dispatch({type: 'UPDATE_OUTFIT', payload: outfit});
        }));
    } else {
      return this.outfitApi.uploadOutfit(outfit)
        .pipe(tap(res => {
          this.outfitStore.dispatch({type: 'ADD_OUTFIT', payload: res});
        }));
    }
  }

  private prepareData(outfitMetadata: OutfitMetadataFormData, outfitId?: UUID) {
    const clothingIds = Object.values(outfitMetadata.clothingGroups)
      .flat()
      .filter(x => x.selected)
      .flatMap(x => x.id);

    const outfit: LocalOutfit = {
      id: outfitId,
      colors: outfitMetadata.colors.map(color => color.value),
      tags: outfitMetadata.tags.map(tag => tag.value),
      seasons: outfitMetadata.seasons,
      mood: outfitMetadata.mood,
      sport: outfitMetadata.outfitDestination.sport,
      images: this.fileHandler.files(),
      clothing: clothingIds
    }
    return outfit;
  }
}
