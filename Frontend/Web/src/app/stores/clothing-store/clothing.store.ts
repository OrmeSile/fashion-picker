import {computed, Injectable, Signal, signal, WritableSignal} from '@angular/core';
import {Clothing} from '../../../types/clothing.types';
import {UUID} from '../../../types/shared.types';
import {Store, StoreReducer} from '../../../types/store.types';
import {ClothingStoreAction} from '../../../types/store-actions.types';

@Injectable({
  providedIn: 'root',
})
export class ClothingStore implements Store<Clothing[], ClothingStoreAction> {

  #stateInternal: WritableSignal<Clothing[]> = signal([]);
  state: Signal<Clothing[]> = computed(() => this.#stateInternal());

  dispatch(action: ClothingStoreAction): void{
    switch (action.type) {
      case "REMOVE_CLOTHING":
        this.#stateInternal.update(state => this.removeClothing(state, action.payload))
        break;
      case "ADD_CLOTHING":
        this.#stateInternal.update(state => this.addClothing(state, action.payload))
        break;
      default: throw new Error("Unknown action type");
    }
  }



  private addClothing: StoreReducer<Clothing[], Clothing[]> = (state: Clothing[], payload: Clothing[]): Clothing[] => {
    return [...state, ...payload];
  }

  private removeClothing: StoreReducer<Clothing[], UUID> = (state: Clothing[], payload: UUID) => {
    return state.filter(clothing => clothing.id !== payload);
  };
}


