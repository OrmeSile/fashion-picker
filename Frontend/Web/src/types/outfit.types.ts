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

type Outfit = OutfitPostResponse & {};

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


type OutfitPostResponse = {
  tags: string[],
  seasons: string[],
  colors: string[],
  id: UUID,
  mood: Mood,
  sport: boolean,
  images: ImageDto[],
  clothing: ClothingDto[],
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

export type {Outfit, LocalOutfit, OutfitDTO, OutfitMetadataFormData, FormTag, Season, Mood, OutfitPostResponse};
