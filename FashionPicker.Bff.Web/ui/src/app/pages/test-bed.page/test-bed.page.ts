import {Component, inject, OnInit} from '@angular/core';
import {AlertMessageOverlay} from '../../components/shared/alert-message-overlay/alert-message-overlay';
import {AlertMessageQueue} from '../../services/alert-message-queue/alert-message-queue';

@Component({
  selector: 'fp-test-bed-page',
  imports: [
    AlertMessageOverlay
  ],
  templateUrl: './test-bed.page.html',
  styleUrl: './test-bed.page.scss',
})
export class TestBedPage {

  messageQueue = inject(AlertMessageQueue);
  ngAfterViewInit(): void {
    this.messageQueue.sendInformation("This is an information message");
  }

  protected sendMessage() {
    this.messageQueue.sendInformation("This is an information message");
  }
}
