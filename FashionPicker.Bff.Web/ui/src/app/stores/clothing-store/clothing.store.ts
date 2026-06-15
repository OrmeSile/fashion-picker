import {computed, inject, Injectable, Signal, signal} from '@angular/core';
import {Clothing} from '../../../types/clothing.types';
import {UUID} from '../../../types/shared.types';
import {Store, StoreReducer} from '../../../types/store.types';
import {ClothingStoreAction} from '../../../types/store-actions.types';
import {ClothingApi} from '../../services/api/clothing-api/clothing-api';

@Injectable({
  providedIn: 'root',
})
export class ClothingStore implements Store<Clothing, ClothingStoreAction> {

  #stateInternal = signal<Clothing[]>([]);
  state: Signal<Clothing[]> = computed(() => this.#stateInternal());

  constructor() {
    const clothingApi = inject(ClothingApi);
    clothingApi.getAllClothing().subscribe(clothing => {
      this.#stateInternal.set(clothing.clothing);
    })
  }

  dispatch(action: ClothingStoreAction): void{
    switch (action.type) {
      case "REMOVE_CLOTHING":
        this.#stateInternal.update(state => this.#removeClothing(state, action.payload))
        break;
      case "ADD_CLOTHING":
        this.#stateInternal.update(state => this.#addClothing(state, action.payload))
        break;
      default: throw new Error("Unknown action type");
    }
  }

  #addClothing: StoreReducer<Clothing[], Clothing[]> = (state: Clothing[], payload?: Clothing[]): Clothing[] => {
    if(!payload)
      return state;
    return [...state, ...payload];
  }

  #removeClothing: StoreReducer<Clothing[], UUID> = (state: Clothing[], payload?: UUID) => {
    if(!payload)
      return state;
    return state.filter(clothing => clothing.id !== payload);
  };
}


