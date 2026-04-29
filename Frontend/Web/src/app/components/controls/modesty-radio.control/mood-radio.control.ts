import {Component, input, model } from '@angular/core';
import {FieldTree, FormField, FormValueControl} from '@angular/forms/signals';
import {Mood} from '../../../../types/outfit.types';

@Component({
  selector: 'fp-mood-radio-control',
  templateUrl: './mood-radio.control.html',
  styleUrl: './mood-radio.control.scss',
  imports: [
    FormField
  ]
})
export class MoodRadioControl implements FormValueControl<Mood>{
  readonly field = input.required<FieldTree<Mood, string>>();
  readonly value = model<Mood>('high');
  readonly controlValue = input.required<string>();
}
