import {UUID} from './shared.types';
import {OutfitFile} from './files.types';

type Outfit = {
  id?: UUID,
  seasons?: SeasonsFormSelector,
  images: OutfitFile[],
  colors?: string[],
  tags?: string[],
  mood: Mood,
  sport: boolean,
};

type OutfitDTO = {
  id?: UUID,
  seasons?: SeasonsFormSelector,
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

type FormTag = {
  id: UUID;
  value: string;
}

type Mood = 'low' | 'medium' | 'high';

type Season = 'spring' | 'summer' | 'autumn' | 'winter';

type SeasonsFormSelector = {
  [TSeason in Season]: boolean;
};

type OutfitDestination ='sport';

type OutfitDestinationFormSelector = {
  [TOutfitDestination in OutfitDestination]: boolean;
}

export type {Outfit, OutfitDTO, OutfitMetadataFormData, FormTag, Season, Mood};
