import {AfterViewInit, Component, inject, signal} from '@angular/core';
import {AlertMessageQueue} from '../../../services/alert-message-queue/alert-message-queue';
import {AlertMessage} from '../../../../types/shared.types';
import {AlertMessageTooltip} from '../alert-message/alert-message-tooltip.component';

@Component({
  selector: 'fp-alert-message-overlay',
  templateUrl: './alert-message-overlay.html',
  styleUrl: './alert-message-overlay.scss',
  imports: [
    AlertMessageTooltip
  ]
})
export class AlertMessageOverlay implements AfterViewInit {
  alertMessageQueue = inject(AlertMessageQueue);
  messages = signal<AlertMessage[]>([]);

  ngAfterViewInit() {
    this.alertMessageQueue.getMessagesStream()
      .subscribe(
        messages => {
          this.messages.update((current) => [...current, messages]);
          let timeout = setTimeout(() => {
            this.messages.update((current) => current.filter((m) => m.id !== messages.id));
            clearTimeout(timeout);
          }, 10000)
        }
      )
  }
}
