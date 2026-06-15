import {computed, Injectable, Signal, signal} from '@angular/core';
import {Outfit} from '../../../types/outfit.types';
import {OutfitStoreAction} from '../../../types/store-actions.types';
import {Store, StoreReducer} from '../../../types/store.types';

@Injectable({
  providedIn: 'root',
})
export class OutfitStore implements Store<Outfit, OutfitStoreAction> {

  #stateInternal = signal<Outfit[]>([]);
  state: Signal<Outfit[]> = computed(() => this.#stateInternal());

  dispatch(action: OutfitStoreAction): void {
    switch (action.type) {
      case "ADD_OUTFIT":
        return this.#stateInternal.update(state => this.#addOutfit(state, action.payload));
      case "ADD_OUTFITS":
        return this.#stateInternal.update(state => this.#addOutfits(state, action.payload));
      case "UPDATE_OUTFIT":
        return this.#stateInternal.update(state => this.#updateOutfit(state, action.payload));
    }
  }

  #addOutfit: StoreReducer<Outfit[], Outfit> = (state: Outfit[], payload?: Outfit | undefined): Outfit[] => {
    if (!payload)
      return state;
    return [...state, payload];
  }
  #addOutfits: StoreReducer<Outfit[], Outfit[]> = (state: Outfit[], payload?: Outfit[] | undefined): Outfit[] => {
    if(!payload)
      return state;
    return [...state, ...payload];
  }
  #updateOutfit:  StoreReducer<Outfit[], Outfit> = (state: Outfit[], payload?: Outfit | undefined): Outfit[] => {
    if(!payload)
      return state;
    return state.map(outfit => outfit.id === payload.id ? payload : outfit);
  }
}
