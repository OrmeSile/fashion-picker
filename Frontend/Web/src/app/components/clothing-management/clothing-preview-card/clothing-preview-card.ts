import {Component, input, output, signal} from '@angular/core';
import {form, FormField} from '@angular/forms/signals';
import {ImageFile} from '../../../../types/files.types';

@Component({
  selector: 'fp-clothing-preview-card',
  imports: [
    FormField
  ],
  templateUrl: './clothing-preview-card.html',
  styleUrl: './clothing-preview-card.scss',
})
export class ClothingPreviewCard {
  file = input<ImageFile>();
  clothingTypeSelected = output<string>();

  private clothingTypeModel = signal({
    clothingType: ""
  });

  protected form = form(this.clothingTypeModel);

  protected onSubmit(event: SubmitEvent) {
    event.preventDefault();
    this.clothingTypeSelected.emit(this.clothingTypeModel().clothingType)
  }
}
