import {Injectable} from '@angular/core';
import {Subject} from 'rxjs';
import type {AlertMessage} from '../../../types/shared.types';

@Injectable({
  providedIn: 'root',
})
export class AlertMessageQueue {
  private messageCount = 0;
  #messages = new Subject<AlertMessage>();

  getMessagesStream(){
    return this.#messages.asObservable();
  }

  sendWarning(text: string) {
    this.messageCount++;
    this.#messages.next({id: this.messageCount, type: "warning", text});
  }

  sendInformation(text: string) {
    this.messageCount++;
    this.#messages.next({id: this.messageCount, type: "information", text});
  }

  sendError(text: string) {
    this.messageCount++;
    this.#messages.next({id: this.messageCount, type: "error", text});
  }
}
