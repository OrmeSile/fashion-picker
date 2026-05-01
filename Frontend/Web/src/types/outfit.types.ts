import {ImageDto, UUID} from './shared.types';
import {ImageFile} from './files.types';

type LocalOutfit = {
  id?: UUID,
  seasons?: SeasonsFormSelector,
  images: ImageFile[],
  colors?: string[],
  tags?: string[],
  mood: Mood,
  sport: boolean,
};

type Outfit = OutfitPostResponse & {};

type OutfitDTO = {
  id?: UUID,
  seasons?: string[],
  colors?: string[],
  tags?: string[],
  mood: Mood,
  sport: boolean,
};


type OutfitMetadataFormData = {
  seasons: SeasonsFormSelector;
  colors: FormTag[];
  tags: FormTag[];
  mood: Mood;
  outfitDestination: OutfitDestinationFormSelector;
}

type OutfitPostResponse = {
  tags: string[],
  seasons: string[],
  colors: string[],
  id: UUID,
  mood: Mood,
  sport: boolean,
  images: ImageDto[]
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
