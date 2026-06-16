import {ChangeDetectionStrategy, Component, input, model, OnInit} from '@angular/core';
import {FormCheckboxControl} from '@angular/forms/signals';
import {FormClothing} from '../../../../types/clothing.types';

@Component({
  selector: 'fp-clothing-checkbox-control',
  imports: [],
  templateUrl: './clothing.checkbox.control.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrl: './clothing.checkbox.control.scss',
})

export class ClothingCheckboxControl implements FormCheckboxControl, OnInit {

  ngOnInit(): void {
    this.checked.update(() => this.clothing().selected);
  }

  readonly clothing = input.required<FormClothing>();
  readonly checked = model<boolean>(false);

  toggle(){
    this.checked.update((val) => !val);
  }

  protected readonly onkeydown = onkeydown;
}
