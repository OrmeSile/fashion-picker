import {Component, input, output, signal} from '@angular/core';

@Component({
  selector: 'fp-preview-image',
  imports: [],
  templateUrl: './preview-image.html',
  styleUrl: './preview-image.scss',
  host: {'[class.selected]': 'selected()'},
})
export class PreviewImage {
  imageUrl = input.required<string>();
  selected = input<boolean>(false);
  removeImage = output<void>();
  openImageDetails = output<void>();

  protected isActive = signal(false);

  protected handleRemoveImage(event: MouseEvent) {
    event.stopPropagation();
    this.removeImage.emit();
  }

  protected handleOpenImageDetails(event: MouseEvent) {
    event.stopPropagation();
    this.openImageDetails.emit();
  }
}

