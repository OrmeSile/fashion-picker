import {ImageDto, UUID} from './shared.types';
import {ImageFile} from './files.types';
import {Clothing, ClothingDto, ClothingFormGroups} from './clothing.types';

type LocalOutfit = {
  id?: UUID,
  seasons?: SeasonsFormSelector,
  images: ImageFile[],
  colors?: string[],
  tags?: string[],
  mood: Mood,
  sport: boolean,
  clothing: UUID[]
};

type Outfit = {
  tags: string[],
  seasons: Season[],
  colors: string[],
  id: UUID,
  mood: Mood,
  sport: boolean,
  images: ImageDto[],
  clothing: ClothingDto[],
}

type OutfitDTO = {
  id?: UUID,
  seasons?: string[],
  colors?: string[],
  tags?: string[],
  mood: Mood,
  sport: boolean,
  clothing: UUID[]
};


type OutfitMetadataFormData = {
  seasons: SeasonsFormSelector;
  tags: FormTag[];
  colors: FormTag[];
  mood: Mood;
  outfitDestination: OutfitDestinationFormSelector;
  clothingGroups: ClothingFormGroups;
}

type FormTag = {
  id: UUID;
  value: string;
}

type Mood = 'low' | 'medium' | 'high';

type Season = 'spring' | 'summer' | 'autumn' | 'winter';

type SeasonsFormSelector = {
  [TSeason in Season]: boolean;
};

type OutfitDestination = 'sport';

type OutfitDestinationFormSelector = {
  [TOutfitDestination in OutfitDestination]: boolean;
}

export type {Outfit, LocalOutfit, OutfitDTO, OutfitMetadataFormData, FormTag, Season, Mood };
