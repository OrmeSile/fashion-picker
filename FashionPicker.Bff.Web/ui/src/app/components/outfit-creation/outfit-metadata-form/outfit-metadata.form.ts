import {Component, computed, ElementRef, input, linkedSignal, output, viewChild} from '@angular/core';
import {form, FormField} from '@angular/forms/signals';
import {FormTag, Outfit, OutfitMetadataFormData} from '../../../../types/outfit.types';
import {OutfitTagControl} from '../../controls/outfit-tag-control/outfit-tag.control';
import {SeasonControl} from '../../controls/season-control/season.control';
import {DestinationControl} from '../../controls/destination.control/destination.control';
import {StyledFormFieldset} from '../../shared/styled-form-fieldset/styled-form-fieldset';
import {MoodRadioControl} from '../../controls/modesty-radio.control/mood-radio.control';
import {Clothing, ClothingFormGroups, FormClothing} from '../../../../types/clothing.types';
import {ClothingCheckboxControl} from '../../controls/clothing.checkbox.control/clothing.checkbox.control';

@Component({
  selector: 'fp-outfit-metadata-form',
  imports: [
    FormField,
    OutfitTagControl,
    SeasonControl,
    DestinationControl,
    StyledFormFieldset,
    MoodRadioControl,
    ClothingCheckboxControl,
  ],
  templateUrl: './outfit-metadata.form.html',
  styleUrl: './outfit-metadata.form.scss',
})
export class OutfitMetadataForm {

  clothing = input<Clothing[]>();
  initialState = input<Outfit | undefined>(undefined);

  submitted = output<OutfitMetadataFormData>();

  formClothing = computed(() => this.clothing()
    ?.map<FormClothing>(c => ({...c, selected: false})));

  clothingFormGroups = computed(() => {
    return this.formClothing()?.reduce<ClothingFormGroups>((prev: ClothingFormGroups, curr: FormClothing) => {
        return ({
          ...prev,
          [curr.clothingType]: [...prev[curr.clothingType], {...curr, selected: false}]
        });
      }, {Top: [], Bottom: [], Shoes: [], Jewelry: [], Fullbody: []});
  });

  emptyStateWithClothingGroups = linkedSignal<ClothingFormGroups | undefined, OutfitMetadataFormData>({
    source: this.clothingFormGroups,
    computation: (baseModel) => {
      return {
        seasons: {
          spring: false,
          summer: false,
          autumn: false,
          winter: false,
        },
        tags: [],
        colors: [],
        mood: 'high',
        outfitDestination: {
          sport: false
        },
        clothingGroups: baseModel ?? {
          Top: [],
          Bottom: [],
          Shoes: [],
          Jewelry: [],
          Fullbody: []
        }
      }
    }
  })

  initialModelState = linkedSignal<Outfit | undefined, OutfitMetadataFormData>({
    source: this.initialState, computation: (outfit): OutfitMetadataFormData => {
      if (!outfit)
        return this.emptyStateWithClothingGroups();

      const seasons = this.emptyStateWithClothingGroups().seasons;

      for (const season of outfit.seasons)
        seasons[season] = true;

      const clothingGroups = this.emptyStateWithClothingGroups().clothingGroups;

      outfit.clothing.forEach(clothing => {
        clothingGroups[clothing.clothingType] = clothingGroups[clothing.clothingType]
          .map(formClothing => formClothing.id === clothing.id ? {...formClothing, selected: true} : formClothing)
      });

      return {
        ...this.emptyStateWithClothingGroups(),
        seasons: seasons,
        tags: outfit.tags.map(tag => ({id: crypto.randomUUID(), value: tag})),
        colors: outfit.colors.map(color => ({id: crypto.randomUUID(), value: color})),
        mood: outfit.mood,
        outfitDestination: {sport: outfit.sport},
        clothingGroups: clothingGroups
      }
    }
  })

  outfitMetadataForm = form(this.initialModelState);

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
    this.initialModelState.update(outfitMetadata => {
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

    this.initialModelState.update(outfitMetadata => {
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
}
