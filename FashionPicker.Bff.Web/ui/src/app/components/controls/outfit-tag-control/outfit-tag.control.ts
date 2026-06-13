import {AfterViewInit, Component, ElementRef, input, model, output, viewChild} from '@angular/core';
import {FormValueControl} from '@angular/forms/signals';
import {FocusOptions} from '@angular/cdk/a11y';

@Component({
  selector: 'fp-outfit-tag-control',
  imports: [],
  templateUrl: './outfit-tag.control.html',
  styleUrl: './outfit-tag.control.scss',
})

export class OutfitTagControl implements FormValueControl<string>, AfterViewInit {

  value = model('');
  readonly inputElementRef = viewChild<ElementRef<HTMLInputElement>>('focusTarget');
  readonly disabled = input<boolean>(false);

  readonly deleteCalled = output<void>();
  readonly blurredWhenEmpty = output<void>();
  readonly editFinished = output<void>();

  ngAfterViewInit(): void {
    if (this.disabled())
      return;

    this.focus();
  }

  focus(options?: FocusOptions): void {
    this.inputElementRef()
      ?.nativeElement
      .focus(options);
  }

  protected handleInput(event: Event) {
    if (this.disabled()) {
      return;
    }

    const target = event.target as HTMLInputElement;
    this.value.set(target.value);
  }

  protected handleBlur(_: Event) {
    if (this.disabled())
      return;

    if (!this.value() || this.value()
      .trim().length === 0)
      this.blurredWhenEmpty.emit();
  }

  protected handleKeydownEnter(event: Event) {
    if (this.disabled()) {
      return;
    }

    event.stopPropagation();
    this.editFinished.emit();
  }

  protected handleKeydownEscape(event: Event) {
    if (this.disabled()) {
      return;
    }

    event.stopPropagation();
    this.deleteCalled.emit();
  }

  protected delete() {
    this.deleteCalled.emit();
  }


}
