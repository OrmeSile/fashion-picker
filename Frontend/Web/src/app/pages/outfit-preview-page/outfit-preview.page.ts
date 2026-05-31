import {Component, computed, inject, OnInit, signal} from '@angular/core';
import {OutfitMetadataForm} from '../../components/outfit-creation/outfit-metadata-form/outfit-metadata.form';
import {FileHandler} from '../../services/file-handler/file-handler';
import {ImageFile} from '../../../types/files.types';
import {LocalOutfit, Outfit, OutfitMetadataFormData} from '../../../types/outfit.types';
import {OutfitStore} from '../../stores/outfit-store/outfit.store';
import {OutfitApi} from '../../services/api/outfit-api/outfit-api';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';
import {ClothingStore} from '../../stores/clothing-store/clothing.store';
import {UUID} from '../../../types/shared.types';
import {ActivatedRoute} from '@angular/router';

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
export class OutfitPreviewPage implements OnInit {


  private activatedRoute = inject(ActivatedRoute);
  private outfitId: UUID | undefined = this.activatedRoute.snapshot.params['id'];

  private outfitStore = inject(OutfitStore);
  fileHandler = inject(FileHandler);
  private outfitApi = inject(OutfitApi);
  protected clothingStore = inject(ClothingStore);

  imagesMetadata = computed(() => this.fileHandler.files());

  protected readonly activeImage = signal<ImageFile | undefined>(undefined);
  protected readonly initialState = signal<Outfit | undefined>(undefined);

  ngOnInit(): void {
    if(!this.outfitId)
      return;
    const outfit = this.outfitStore.state().find(x => x.id === this.outfitId);
    if(outfit)
      return this.initialState.set(outfit);

    this.outfitApi.getOutfitById(this.outfitId).subscribe(outfit => {
      this.outfitStore.dispatch({type: 'ADD_OUTFIT', payload: outfit});
      this.initialState.set(this.outfitStore.state().find(x => x.id === this.outfitId));
    });
  }

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
    this.setActiveImage(this.imagesMetadata()[0]);
  }

  protected setActiveImage(metadata: ImageFile) {
    this.activeImage.set(metadata);
  }

  protected handleSubmitted(outfitMetadata: OutfitMetadataFormData) {

    const clothingIds = Object.values(outfitMetadata.clothingGroups)
      .flat()
      .filter(x => x.selected)
      .flatMap(x => x.id);

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
          this.outfitStore.dispatch({type: 'ADD_OUTFIT', payload: res});
        }
      );
  }
}
