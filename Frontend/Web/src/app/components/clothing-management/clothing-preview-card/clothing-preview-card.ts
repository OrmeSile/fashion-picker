import {Component, input, output, signal} from '@angular/core';
import {form, FormField} from '@angular/forms/signals';
import {ImageFile} from '../../../../types/files.types';

@Component({
  selector: 'fp-clothing-preview-card',
  imports: [
    FormField
  ],
  host: {
    '[style.background]':'loading() ? "yellow" : success() ? "green" : "#333"'
  },
  templateUrl: './clothing-preview-card.html',
  styleUrl: './clothing-preview-card.scss',
})
export class ClothingPreviewCard {

  file = input<ImageFile>();
  loading = input(false);
  success = input(false);

  clothingTypeSelected = output<string>();


  private clothingTypeModel = signal({
    clothingType: ""
  });

  protected form = form(this.clothingTypeModel);

  protected onSubmit(event: SubmitEvent) {
    event.preventDefault();

    this.clothingTypeSelected.emit(this.clothingTypeModel().clothingType);
  }
}
