import {UUID} from './shared.types';

type TechnicalOutfitMetadata = {
  id: UUID,
  fileUrl: string
};

type CustomisableOutfitMetadata = {
  [key: string]: string[]
}

type OutfitMetadata = TechnicalOutfitMetadata & CustomisableOutfitMetadata;

type OutfitFile = TechnicalOutfitMetadata & {
  file: File;
};

export type { TechnicalOutfitMetadata, CustomisableOutfitMetadata, OutfitMetadata, OutfitFile };
