import {UUID} from './shared.types';
import {OutfitFile} from './files.types';

type Outfit = {
  id?: UUID,
  imageUrls: string[],
  seasons?: SeasonsFormSelector,
  colors?: string[],
  tags?: string[],
  images?: OutfitFile[]
};


type OutfitMetadataFormData = {
  seasons: SeasonsFormSelector;
  colors: FormTag[];
  tags: FormTag[];
  modesty: number;
  outfitDestination: OutfitDestinationFormSelector;
}

type FormTag = {
  id: UUID;
  value: string;
}

type Season = 'spring' | 'summer' | 'autumn' | 'winter';

type SeasonsFormSelector = {
  [TSeason in Season]: boolean;
};

type OutfitDestination = 'outing' | 'sport';

type OutfitDestinationFormSelector = {
  [TOutfitDestination in OutfitDestination]: boolean;
}

export type {Outfit, OutfitMetadataFormData, FormTag, Season};
