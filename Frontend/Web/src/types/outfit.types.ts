import {UUID} from './shared.types';
import {OutfitFile} from './files.types';

type Outfit = {
  id?: UUID,
  imageUrls: string[],
  season?: string,
  colors?: string[],
  tags?: string[],
  images?: OutfitFile[]
};

export type {Outfit};
