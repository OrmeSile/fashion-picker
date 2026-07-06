import {Component, input} from '@angular/core';

@Component({
  selector: 'fp-alert-message-tooltip',
  imports: [],
  templateUrl: './alert-message-tooltip.component.html',
  styleUrl: './alert-message-tooltip.component.scss',
})
export class AlertMessageTooltip {
  text = input.required<string>();
}
