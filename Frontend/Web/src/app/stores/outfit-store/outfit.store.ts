import {computed, Injectable, signal} from '@angular/core';
import {Outfit} from '../../../types/outfit.types';

@Injectable({
  providedIn: 'root',
})
export class OutfitStore {
  private outfitsInternal = signal<Outfit[]>([]);

  unsavedOutfits = computed(() => this.outfitsInternal().filter((outfit) => !outfit.id));
  savedOutfits = computed(() => this.outfitsInternal().filter((outfit) => outfit.id));
  outfits = computed(() => this.outfitsInternal());

  addOutfit(outfit: Outfit) {
    this.outfitsInternal.update(outfits => [...outfits, outfit]);
  }
}
