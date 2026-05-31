import {StoreAction} from './store.types';
import {Clothing} from './clothing.types';
import {UUID} from './shared.types';
import {Outfit} from './outfit.types';

type ClothingStoreAction =
  StoreAction<'ADD_CLOTHING', Clothing[]> |
  StoreAction<'REMOVE_CLOTHING', UUID>
  ;

type OutfitStoreAction =
  StoreAction<'ADD_OUTFIT', Outfit> |
  StoreAction<'ADD_OUTFITS', Outfit[]> |
  StoreAction<'UPDATE_OUTFIT', Outfit>
  ;

export type {ClothingStoreAction, OutfitStoreAction};
