import {Component, input, output, signal} from '@angular/core';
import {disabled, form, FormField} from '@angular/forms/signals';
import {ImageFile} from '../../../../types/files.types';
import {TitleCasePipe} from '@angular/common';

@Component({
  selector: 'fp-clothing-preview-card',
  imports: [
    FormField,
    TitleCasePipe
  ],
  templateUrl: './clothing-preview-card.html',
  styleUrl: './clothing-preview-card.scss',
})
export class ClothingPreviewCard {

  file = input<ImageFile>();
  loading = input(false);
  success = input(false);

  clothingTypeSelected = output<string>();

  protected clothingOptions = ['top', 'bottom', 'shoes', 'jewelry', 'fullbody'];

  private clothingTypeModel = signal({
    clothingType: "top"
  });

  protected form = form(this.clothingTypeModel, (schemaPath) => {
    disabled(schemaPath.clothingType, () => this.loading())
  });

  protected onSubmit(event: SubmitEvent) {
    event.preventDefault();

    this.clothingTypeSelected.emit(this.clothingTypeModel().clothingType);
  }
}
