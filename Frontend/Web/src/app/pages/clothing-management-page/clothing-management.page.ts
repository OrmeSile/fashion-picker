import {Component, computed, inject, signal} from '@angular/core';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';
import {FileHandler} from '../../services/file-handler/file-handler';
import {ImageFile} from '../../../types/files.types';
import {UUID} from '../../../types/shared.types';
import {ClothingPreviewCard} from '../../components/clothing-management/clothing-preview-card/clothing-preview-card';
import {ClothingStore} from '../../stores/clothing-store/clothing.store';
import {ClothingApi} from '../../services/api/clothing-api/clothing-api';
import {LocalClothing} from '../../../types/clothing.types';

@Component({
  selector: 'fp-clothing-management-page',
  imports: [
    DropZone,
    ClothingPreviewCard
  ],
  providers: [FileHandler],
  templateUrl: './clothing-management.page.html',
  styleUrl: './clothing-management.page.scss',
})
export class ClothingManagementPage {

  fileHandler = inject(FileHandler);
  clothingStore = inject(ClothingStore);
  clothingApi = inject(ClothingApi);

  savedClothing = this.clothingStore.state;
  files = computed(() => this.fileHandler.files());
  activeImage = signal<ImageFile | undefined>(undefined);

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

  protected setActiveImage(outfitFile: ImageFile) {
    this.activeImage.set(outfitFile);
  }

  protected handleClothingTypeSelected(event: { selected: string; id: UUID}) {
    const imageFile = this.files().find(file => file.id === event.id);
    if(!imageFile)
      return;
    const localClothing: LocalClothing = {
      clothingType: event.selected,
      files: [imageFile.file]
    }
    this.clothingApi.uploadClothing(localClothing)
      .subscribe(res => {
        this.clothingStore.dispatch({type: 'ADD_CLOTHING', payload: [res]})
      });
  }
}
