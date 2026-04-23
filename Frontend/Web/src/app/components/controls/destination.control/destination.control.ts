import {Component, input, model} from '@angular/core';
import {FormCheckboxControl} from '@angular/forms/signals';

@Component({
  selector: 'fp-destination-control',
  imports: [],
  templateUrl: './destination.control.html',
  styleUrl: './destination.control.scss',
})
export class DestinationControl implements FormCheckboxControl {
  readonly checked = model(false);
  readonly label = input.required<string>();

  toggle(){
    this.checked.update((val) => !val);
  }
}
