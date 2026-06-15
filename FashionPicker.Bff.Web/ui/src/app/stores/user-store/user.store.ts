import {computed, Injectable, signal, Signal} from '@angular/core';
import {Store} from '../../../types/store.types';
import {UserStoreAction} from '../../../types/store-actions.types';

interface User {
  name: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserStore implements Store<User, UserStoreAction> {
    #stateInternal = signal<User | undefined>(undefined);
    state = computed(() => this.#stateInternal());

    isLoggedIn = computed(() => this.#stateInternal() !== undefined);

    dispatch(action: UserStoreAction): void {
      switch (action.type){
        case 'SET_USER':
          return this.#stateInternal.set(action.payload);
        case 'CLEAR_USER':
          return this.#stateInternal.set(undefined);
      }
    }
}
