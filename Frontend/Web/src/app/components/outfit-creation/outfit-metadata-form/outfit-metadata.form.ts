import {Component, computed, ElementRef, input, linkedSignal, output, viewChild} from '@angular/core';
import {form, FormField} from '@angular/forms/signals';
import {FormTag, OutfitMetadataFormData} from '../../../../types/outfit.types';
import {OutfitTagControl} from '../../controls/outfit-tag-control/outfit-tag.control';
import {SeasonControl} from '../../controls/season-control/season.control';
import {DestinationControl} from '../../controls/destination.control/destination.control';
import {StyledFormFieldset} from '../../shared/styled-form-fieldset/styled-form-fieldset';
import {MoodRadioControl} from '../../controls/modesty-radio.control/mood-radio.control';
import {Clothing, ClothingFormGroups} from '../../../../types/clothing.types';

@Component({
  selector: 'fp-outfit-metadata-form',
  imports: [
    FormField,
    OutfitTagControl,
    SeasonControl,
    DestinationControl,
    StyledFormFieldset,
    MoodRadioControl,
  ],
  templateUrl: './outfit-metadata.form.html',
  styleUrl: './outfit-metadata.form.scss',
})
export class OutfitMetadataForm {

  submitted = output<OutfitMetadataFormData>();

  clothingInput = input<Clothing[]>();

  clothingGroups = computed(() => {
    return this.clothingInput()
      ?.reduce<ClothingFormGroups>((prev: ClothingFormGroups, curr: Clothing) => {
        if (!(curr.clothingType in prev)) {
          return prev;
        }
        return ({
          ...prev,
          [curr.clothingType]: {
            ...prev[curr.clothingType], [curr.id]: false
          }
        })
      }, {Top: {}, Bottom: {}, Shoes: {}, Jewelry: {}, FullBody: {}})
  });

  outfitMetadataModel = linkedSignal({
      source: this.clothingGroups,
      computation: (baseModel) => {
        return {
          seasons: {
            spring: false,
            summer: false,
            autumn: false,
            winter: false
          },
          tags: [],
          colors: [],
          mood: 'high',
          outfitDestination: {
            sport: false
          },
          clothingGroups: baseModel ?? {
            Top: {},
            Bottom: {},
            Shoes: {},
            Jewelry: {},
            FullBody: {}
          }
        } as OutfitMetadataFormData
      }
    }
  );

  outfitMetadataForm = form(this.outfitMetadataModel);

  addTagButtonRef = viewChild<ElementRef<HTMLInputElement>>('addTagButton');
  addColorButtonRef = viewChild<ElementRef<HTMLInputElement>>('addColorButton');

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
      colors: this.outfitMetadataForm.colors()
        .value(),
      tags: this.outfitMetadataForm.tags()
        .value(),
      seasons: this.outfitMetadataForm.seasons()
        .value(),
      mood: this.outfitMetadataForm.mood()
        .value(),
      outfitDestination: this.outfitMetadataForm.outfitDestination()
        .value(),
      clothingGroups: this.outfitMetadataForm.clothingGroups()
        .value(),
    };

    this.submitted.emit(outfitMetadata);
  }

  protected onKeyUp(event: KeyboardEvent) {
    event.preventDefault();
    event.stopPropagation();
  }

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

  protected getSmallImagePathForClothingId(id: string) {
    return this.clothingInput()
      ?.find(x => x.id === id)?.images[0].small ?? "";
  }

  protected handleColorEditFinished() {
    this.addColorButtonRef()
      ?.nativeElement
      .focus();
  }
}
