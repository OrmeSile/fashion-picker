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

type ClothingDto = Clothing & {}

type OutfitClothingPostResponse = {
  id: UUID;
}

type ClothingGetAllResponse = {clothing: ClothingDto[]}

export type {Clothing, LocalClothing, ClothingDto, OutfitClothingPostResponse, ClothingGetAllResponse}
