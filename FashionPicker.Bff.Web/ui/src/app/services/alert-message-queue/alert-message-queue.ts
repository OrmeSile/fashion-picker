import {Injectable} from '@angular/core';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AlertMessageQueue {
  private messageCount = 0;
  #messages = new Subject<{id: number, text: string}>();

  getMessagesStream(){
    return this.#messages.asObservable();
  }

  sendWarning(text: string) {
    this.messageCount++;
    this.#messages.next({id: this.messageCount, text});
  }

  sendInformation(text: string) {
    this.messageCount++;
    this.#messages.next({id: this.messageCount, text});
  }

  sendError(text: string) {
    this.messageCount++;
    this.#messages.next({id: this.messageCount, text});
  }
}
