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

type FormClothing = Clothing & {selected: boolean}

type ClothingFormGroups = {[key in ClothingType]: FormClothing[]};

type ClothingType = "Top" | "Bottom" | "Shoes" | "Jewelry" | "Fullbody";

export type {Clothing, LocalClothing, ClothingDto, FormClothing, ClothingFormGroups}

