import {Component, computed, inject, input, signal} from '@angular/core';

import {UUID} from '../../../types/shared.types';
import {TechnicalOutfitMetadata} from '../../../types/files.types';
import {ImageAlbumGridPreview} from './image-album-grid-preview/image-album-grid-preview';
import {PreviewImage} from './image-album-grid-preview/preview-image/preview-image';
import {FileHandler} from './file-handler/file-handler';
import {form, FormField, submit} from '@angular/forms/signals';
import {OutfitTag} from './outfit-tag/outfit-tag';
import {DropUpload} from './drop-zone/drop-upload';
import {OutfitStore} from '../outfit-store/outfit-store';
import {Outfit} from '../../../types/outfit.types';

@Component({
  selector: 'fp-outfit-preview',
  imports: [
    ImageAlbumGridPreview,
    PreviewImage,
    DropUpload,
    FormField,
    OutfitTag
  ],
  providers: [FileHandler],
  templateUrl: './outfit-preview.html',
  styleUrl: './outfit-preview.scss',
})
export class OutfitPreview {

  private outfitStore = inject(OutfitStore);
  fileHandler = inject(FileHandler);

  outfitMetadataModel = signal<OutfitMetadataFormData>({
      name: '',
      description: '',
      season: '',
      tags: [],
      colors: []
    }
  )

  outfitMetadataForm = form(this.outfitMetadataModel);

  imagesMetadata = computed(() => this.fileHandler.files());

  protected readonly activeImage = signal<TechnicalOutfitMetadata | undefined>(undefined);

  protected handleDataTransfer($event: FileList) {
    this.fileHandler.addFiles($event);
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

  protected addTag(event: MouseEvent) {
    event.preventDefault();
    this.outfitMetadataModel.update(model => {
      return {...model, tags: [...model.tags, '']}
    });
  }

  protected addColor(event: MouseEvent) {
    event.preventDefault();
    this.outfitMetadataModel.update(model => {
      return {...model, colors: [...model.colors, '']}
    })
  }

  protected onSubmit(event: SubmitEvent) {
    event.preventDefault();
    const newOutfit: Outfit = {
      id: undefined,
      colors: this.outfitMetadataForm.colors().value(),
      tags: this.outfitMetadataForm.tags().value(),
      season: this.outfitMetadataForm.season().value(),
      imageUrls: this.fileHandler.files().map(file => file.fileUrl),
      images: this.fileHandler.files()
    }
    this.outfitStore.addOutfit(newOutfit);
  }
}

type OutfitMetadataFormData = {
  name: string;
  description: string;
  season: string;
  colors: string[];
  tags: string[];
}
