import {ImageDto, UUID} from './shared.types';

type Clothing = {
  id: UUID;
  clothingType: ClothingType;
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

type ClothingFormGroups = {
  [key in ClothingType]: {[key: UUID]: boolean}
}
type ClothingType = "Top" | "Bottom" | "Shoes" | "Jewelry" | "FullBody";

type ClothingGetAllResponse = { clothing: ClothingDto[] }

export type {Clothing, LocalClothing, ClothingDto, OutfitClothingPostResponse, ClothingGetAllResponse, ClothingFormGroups}
