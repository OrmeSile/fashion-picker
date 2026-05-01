import {StoreAction} from './store.types';
import {Clothing} from './clothing.types';
import {UUID} from './shared.types';

type ClothingStoreAction =
  StoreAction<'ADD_CLOTHING', Clothing[]> |
  StoreAction<'REMOVE_CLOTHING', UUID>;

export type {ClothingStoreAction}
