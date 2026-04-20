import {Component, ElementRef, output, signal, viewChild} from '@angular/core';
import {form, FormField} from '@angular/forms/signals';
import {OutfitMetadataFormData} from '../../../../types/outfit.types';
import {OutfitTagControl} from '../../controls/outfit-tag-control/outfit-tag.control';
import {SeasonControl} from '../../controls/season-control/season.control';

@Component({
  selector: 'fp-outfit-metadata-form',
  imports: [
    FormField,
    OutfitTagControl,
    SeasonControl
  ],
  templateUrl: './outfit-metadata.form.html',
  styleUrl: './outfit-metadata.form.scss',
})
export class OutfitMetadataForm {

  submitted = output<OutfitMetadataFormData>();

  addTagButtonRef = viewChild<ElementRef<HTMLInputElement>>('addTagButton');

  outfitMetadataModel = signal<OutfitMetadataFormData>({
      seasons: {
        spring: false,
        summer: false,
        autumn: false,
        winter: false
      },
      tags: [],
      colors: []
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
          ...outfitMetadata.tags.slice( index + 1)
        ]
      };
    })
  }

  protected handleEditFinished() {
    this.addTagButtonRef()
      ?.nativeElement
      .focus();
  }

  protected addColor(event: MouseEvent) {
    event.preventDefault();
    this.outfitMetadataModel.update(model => {
      return {...model, colors: [...model.colors, '']}
    })
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
    };

    this.submitted.emit(outfitMetadata);
  }

  protected onKeyUp(event: KeyboardEvent) {
    event.preventDefault();
    event.stopPropagation();
  }

  protected readonly console = console;


}
