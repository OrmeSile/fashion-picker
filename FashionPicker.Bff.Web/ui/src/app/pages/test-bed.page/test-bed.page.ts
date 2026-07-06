import {Component} from '@angular/core';
import {AlertMessageOverlay} from '../../components/shared/alert-message-overlay/alert-message-overlay';

@Component({
  selector: 'fp-test-bed-page',
  imports: [
    AlertMessageOverlay
  ],
  templateUrl: './test-bed.page.html',
  styleUrl: './test-bed.page.scss',
})
export class TestBedPage {
}
