import {Component, ElementRef, output, signal, viewChild} from '@angular/core';
import {form, FormField} from '@angular/forms/signals';
import {FormTag, OutfitMetadataFormData} from '../../../../types/outfit.types';
import {OutfitTagControl} from '../../controls/outfit-tag-control/outfit-tag.control';
import {SeasonControl} from '../../controls/season-control/season.control';
import {ModestySliderControl} from '../../controls/modesty-slider.control/modesty-slider.control';
import {DestinationControl} from '../../controls/destination.control/destination.control';

@Component({
  selector: 'fp-outfit-metadata-form',
  imports: [
    FormField,
    OutfitTagControl,
    SeasonControl,
    ModestySliderControl,
    DestinationControl
  ],
  templateUrl: './outfit-metadata.form.html',
  styleUrl: './outfit-metadata.form.scss',
})
export class OutfitMetadataForm {

  submitted = output<OutfitMetadataFormData>();

  addTagButtonRef = viewChild<ElementRef<HTMLInputElement>>('addTagButton');
  addColorButtonRef = viewChild<ElementRef<HTMLInputElement>>('addColorButton');

  outfitMetadataModel = signal<OutfitMetadataFormData>({
      seasons: {
        spring: false,
        summer: false,
        autumn: false,
        winter: false
      },
      tags: [],
      colors: [],
      modesty: 0,
      outfitDestination: {
        outing: false,
        sport: false
      }

    }
  )

  outfitMetadataForm = form(this.outfitMetadataModel);

  addEmptyTag() {
    const tagId = crypto.randomUUID();
    const newTag = {id: tagId, value: ''};
    this.outfitMetadataForm.tags()
      .value
      .update(tags =>
        [...tags, newTag]
      );
  }

  removeTag(index: number) {
    this.outfitMetadataModel.update(outfitMetadata => {
      return {
        ...outfitMetadata, tags: [
          ...outfitMetadata.tags.slice(0, index),
          ...outfitMetadata.tags.slice(index + 1)
        ]
      };
    })
  }

  protected handleTagEditFinished() {
    this.addTagButtonRef()
      ?.nativeElement
      .focus();
  }

  protected addEmptyColor(event: MouseEvent) {
    event.preventDefault();
    const colorId = crypto.randomUUID();
    const newColor: FormTag = {id: colorId, value: ''};
    this.outfitMetadataForm.colors()
      .value
      .update(colors =>
        [...colors, newColor]
      );

  }

  protected onSubmit(event: SubmitEvent) {
    event.preventDefault();

    const outfitMetadata: OutfitMetadataFormData = {
      colors: this.outfitMetadataForm.colors().value(),
      tags: this.outfitMetadataForm.tags().value(),
      seasons: this.outfitMetadataForm.seasons().value(),
      modesty: this.outfitMetadataForm.modesty().value(),
      outfitDestination: this.outfitMetadataForm.outfitDestination().value()
    };

    this.submitted.emit(outfitMetadata);
  }

  protected onKeyUp(event: KeyboardEvent) {
    event.preventDefault();
    event.stopPropagation();
  }

  protected readonly console = console;


  protected removeColor(index: number) {

    this.outfitMetadataModel.update(outfitMetadata => {
      return {
        ...outfitMetadata, colors: [
          ...outfitMetadata.colors.slice(0, index),
          ...outfitMetadata.colors.slice(index + 1)
        ]
      };
    })
  }

  protected handleColorEditFinished() {
    this.addColorButtonRef()
      ?.nativeElement
      .focus();
  }

  protected readonly Object = Object;
}
