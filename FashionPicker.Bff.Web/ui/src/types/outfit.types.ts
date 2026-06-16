import {ImageDto, UUID} from './shared.types';
import {ImageFile} from './files.types';
import {ClothingDto, ClothingFormGroups} from './clothing.types';

interface LocalOutfit {
  id?: UUID,
  seasons?: SeasonsFormSelector,
  images: ImageFile[],
  colors?: string[],
  tags?: string[],
  mood: Mood,
  sport: boolean,
  clothing: UUID[]
}

interface Outfit {
  tags: string[],
  seasons: Season[],
  colors: string[],
  id: UUID,
  mood: Mood,
  sport: boolean,
  images: ImageDto[],
  clothing: ClothingDto[],
}

interface OutfitDTO {
  id?: UUID,
  seasons?: string[],
  colors?: string[],
  tags?: string[],
  mood: Mood,
  sport: boolean,
  clothing: UUID[]
}


interface OutfitMetadataFormData {
  seasons: SeasonsFormSelector;
  tags: FormTag[];
  colors: FormTag[];
  mood: Mood;
  outfitDestination: OutfitDestinationFormSelector;
  clothingGroups: ClothingFormGroups;
}

interface FormTag {
  id: UUID;
  value: string;
}

type Mood = 'low' | 'medium' | 'high';

type Season = 'spring' | 'summer' | 'autumn' | 'winter';

type SeasonsFormSelector = Record<Season, boolean>;

type OutfitDestination = 'sport';

type OutfitDestinationFormSelector = Record<OutfitDestination, boolean>

export type {Outfit, LocalOutfit, OutfitDTO, OutfitMetadataFormData, FormTag, Season, Mood };
