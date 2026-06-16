import {UUID} from './shared.types';

interface TechnicalOutfitMetadata {
  id: UUID,
  fileUrl: string
}

type CustomisableOutfitMetadata = Record<string, string[]>;

type OutfitMetadata = TechnicalOutfitMetadata & CustomisableOutfitMetadata;

type ImageFile = TechnicalOutfitMetadata & {
  file: File;
};

export type { TechnicalOutfitMetadata, CustomisableOutfitMetadata, OutfitMetadata, ImageFile };
