import {AfterViewInit, Component, inject, OnInit, signal} from '@angular/core';
import {AlertMessageQueue} from '../../../services/alert-message-queue/alert-message-queue';
import {AlertMessage} from '../alert-message/alert-message';

@Component({
  selector: 'fp-alert-message-overlay',
  imports: [
    AlertMessage
  ],
  templateUrl: './alert-message-overlay.html',
  styleUrl: './alert-message-overlay.scss',
})
export class AlertMessageOverlay implements AfterViewInit {
  alertMessageQueue = inject(AlertMessageQueue);
  messages = signal<{id: number, text: string}[]>([]);

  ngAfterViewInit() {
    this.alertMessageQueue.getMessagesStream()
      .subscribe(
        messages => {
          this.messages.update((current) => [...current, messages]);
          let timeout = setTimeout(() => {
            this.messages.update((current) => current.filter((m) => m.id !== messages.id));
          }, 3000)
        }
      )
  }
}
