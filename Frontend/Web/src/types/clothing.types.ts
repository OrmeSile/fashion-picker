import {ImageDto, UUID} from './shared.types';

type Clothing = {
  id: UUID;
  clothingType: string;
  images: ImageDto[];
};

type LocalClothing = {
  clothingType: string;
  files: File[];
};

type ClothingPostResponse = {
  id: UUID;
  clothingType: string;
  images: ImageDto[];

}

export type {Clothing, LocalClothing, ClothingPostResponse}
