import {ImageDto, UUID} from './shared.types';

interface Clothing {
  id: UUID;
  clothingType: ClothingType;
  images: ImageDto[];
}

interface LocalClothing {
  clothingType: string;
  files: File[];
}

type ClothingDto = Clothing & {}

type FormClothing = Clothing & {selected: boolean}

type ClothingFormGroups = Record<ClothingType, FormClothing[]>;

type ClothingType = "Top" | "Bottom" | "Shoes" | "Jewelry" | "Fullbody";

export type {Clothing, LocalClothing, ClothingDto, FormClothing, ClothingFormGroups}

