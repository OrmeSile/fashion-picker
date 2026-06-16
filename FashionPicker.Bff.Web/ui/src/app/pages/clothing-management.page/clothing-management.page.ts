import {Component, computed, inject, linkedSignal} from '@angular/core';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';
import {FileHandler} from '../../services/file-handler/file-handler';
import {FileState, UUID} from '../../../types/shared.types';
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

  fileStates = linkedSignal<FileState[],FileState[]>({
    source: computed(() => this.fileHandler.files().map(file => ({file, loading: false, success: false}))),
    computation: (curr, prev) => {
      return curr.reduce((acc: FileState[], current) => {
        const previousStateValue = prev?.value.find(fileState => fileState.file.id === current.file.id);
        return [...acc, {...current, loading: previousStateValue?.loading ?? false, success: previousStateValue?.success ?? false}];
      }, []);
    }
  });

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
  }

  protected handleClothingTypeSelected(event: { selected: string; id: UUID}) {
    const imageFile = this.fileStates().find(file => file.file.id === event.id);
    if(!imageFile)
      return;
    const localClothing: LocalClothing = {
      clothingType: event.selected,
      files: [imageFile.file.file]
    }
    this.fileStates.update(states => states.map(state => state.file.id === event.id ? {...state, loading: true} : state));
    this.clothingApi.uploadClothing(localClothing)
      .subscribe(res => {
        this.clothingStore.dispatch({type: 'ADD_CLOTHING', payload: [res]})
        this.fileStates.update(states => states.map(state => state.file.id === event.id ? {...state, loading: false, success: true} : state));
      });
  }
}

