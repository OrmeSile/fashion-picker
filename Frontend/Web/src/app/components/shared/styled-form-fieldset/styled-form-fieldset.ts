import {Component, input} from '@angular/core';

@Component({
  selector: 'fp-styled-form-fieldset',
  imports: [],
  templateUrl: './styled-form-fieldset.html',
  styleUrl: './styled-form-fieldset.scss',
})
export class StyledFormFieldset {
  readonly legend = input.required<string>();
}
