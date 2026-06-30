import {Component, input, Input} from '@angular/core';

@Component({
  selector: 'fp-alert-message',
  imports: [],
  templateUrl: './alert-message.html',
  styleUrl: './alert-message.scss',
})
export class AlertMessage {
  text = input.required<string>();
  messageId = input.required<number>();
}
