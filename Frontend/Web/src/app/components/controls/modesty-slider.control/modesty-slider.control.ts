import {Component, model} from '@angular/core';
import {FormValueControl} from '@angular/forms/signals';

@Component({
  selector: 'fp-modesty-slider-control',
  imports: [],
  templateUrl: './modesty-slider.control.html',
  styleUrl: './modesty-slider.control.scss',
})
export class ModestySliderControl implements FormValueControl<number> {
  readonly value = model(0);

}
